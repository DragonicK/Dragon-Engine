Attribute VB_Name = "Map_Data"
Option Explicit

Public Const TileSize As Long = 256

Public Const MaxMaps As Long = 10
Public Const MaxMapX As Byte = 24
Public Const MaxMapY As Byte = 18

Public Enum MapMoral
    MapMoral_None
    MapMoral_Safe
    MapMoral_Boss
End Enum

' Dados de todos os mapas.
Public MapPropertyData(1 To MaxMaps) As MapRec
Public CurrentMap As MapRec

Public MaxGroundParallax As Long
Public MaxFringeParallax As Long

Public GroundParallax() As ParallaxRec
Public FringeParallax() As ParallaxRec

Public Type ParallaxRec
    Num As Long
    X As Long
    Y As Long
    Texture As TextureStruct
End Type

Public Type FogRec
    Num As Long
    Opacity As Byte
    Blending As Byte
    Red As Byte
    Green As Byte
    Blue As Byte
End Type

Private Type MapDataRec
    Id As Long
    File As String
    Name As String
    Music As String
    Ambience As String

    Moral As Byte
    Weather As Byte

    Up As Long
    Down As Long
    Left As Long
    Right As Long

    MaxX As Long
    MaxY As Long

    Fog As FogRec
End Type

Public Type TileRec
    Type As Byte
    Data1 As Long
    Data2 As Long
    Data3 As Long
    Data4 As Long
    Data5 As Long
    DirBlock As Byte
End Type

Private Type MapTileRec
    Tile() As TileRec
End Type

Private Type MapRec
    MapData As MapDataRec
    TileData As MapTileRec
End Type

Public MapView As RECT

Public Sub ClearMap()
    Call ZeroMemory(ByVal VarPtr(CurrentMap), LenB(CurrentMap))
    
    CurrentMap.MapData.Name = vbNullString
    CurrentMap.MapData.MaxX = MaxMapX
    CurrentMap.MapData.MaxY = MaxMapY

    ReDim CurrentMap.TileData.Tile(0 To CurrentMap.MapData.MaxX, 0 To CurrentMap.MapData.MaxY)
End Sub

Public Sub LoadMapProperty(ByVal MapNum As Long)
    Const NextByte As Long = 1

    Dim FileName As String, f As Long, X As Long, Y As Long
    Dim Name As String * NAME_LENGTH
    Dim Music As String * NAME_LENGTH
    Dim Ambience As String * NAME_LENGTH
    Dim Position As Long
    Dim MapId As Long

    ' dump tile data
    FileName = App.Path & MAP_PATH & "Map" & MapNum & ".maps"

    f = FreeFile
    Open FileName For Binary As #f

    ' Skip the first bytes.
    Const StarterParallaxIndexSize As Long = 4
    Const VersionSize As Long = 16
    Const DateTimeSize As Long = 20

    Position = StarterParallaxIndexSize + VersionSize + DateTimeSize + NextByte

    Get #f, Position, MapId
    Get #f, , Name
    Get #f, , Music
    Get #f, , Ambience

    ' Read map's moral
    Get #f, , MapPropertyData(MapNum).MapData.Moral
    Get #f, , MapPropertyData(MapNum).MapData.Weather

    Get #f, , MapPropertyData(MapNum).MapData.Fog.Num
    Get #f, , MapPropertyData(MapNum).MapData.Fog.Opacity
    Get #f, , MapPropertyData(MapNum).MapData.Fog.Blending
    Get #f, , MapPropertyData(MapNum).MapData.Fog.Red
    Get #f, , MapPropertyData(MapNum).MapData.Fog.Green
    Get #f, , MapPropertyData(MapNum).MapData.Fog.Blue

    Get #f, , MapPropertyData(MapNum).MapData.MaxX
    Get #f, , MapPropertyData(MapNum).MapData.MaxY

    MapPropertyData(MapNum).MapData.Id = MapId
    MapPropertyData(MapNum).MapData.Name = Trim$(Name)
    MapPropertyData(MapNum).MapData.Music = Trim$(Music)
    MapPropertyData(MapNum).MapData.Ambience = Trim$(Ambience)

    Dim MaxX As Long
    Dim MaxY As Long

    MaxX = MapPropertyData(MapNum).MapData.MaxX - 1
    MaxY = MapPropertyData(MapNum).MapData.MaxY - 1
    
    MapPropertyData(MapNum).MapData.MaxX = MaxX
    MapPropertyData(MapNum).MapData.MaxY = MaxY

    ReDim MapPropertyData(MapNum).TileData.Tile(0 To MaxX, 0 To MaxY) As TileRec

    With MapPropertyData(MapNum)
        For X = 0 To MaxX
            For Y = 0 To MaxY
                Get #f, , .TileData.Tile(X, Y).Type
                Get #f, , .TileData.Tile(X, Y).Data1
                Get #f, , .TileData.Tile(X, Y).Data2
                Get #f, , .TileData.Tile(X, Y).Data3
                Get #f, , .TileData.Tile(X, Y).Data4
                Get #f, , .TileData.Tile(X, Y).Data5
                Get #f, , .TileData.Tile(X, Y).DirBlock
            Next
        Next
    End With

    Close #f
End Sub

Public Sub LoadMapParallax(ByVal MapNum As Long, ByRef Key() As Byte, ByRef IV() As Byte)
    Const NextByte As Long = 1
    
    Dim i As Long
    Dim Settings As AesSettings
    Dim Encrypted() As Byte
    Dim EncryptedLength As Long
    Dim Decrypted() As Byte
    Dim DecryptedLength As Long
    Dim Success As Boolean

    Settings.CipherMode = AesCipherMode.AesCipherMode_CBC
    Settings.KeyBitSize = AesBitSize.AesBitSize_128
    Settings.PaddingMode = AesPaddingMode.AesPaddingMode_PKCS7
    
    For i = 0 To AesKeyLength - 1
        Settings.Key(i) = Key(i)
        Settings.IV(i) = IV(i)
    Next

    Dim FileName As String, f As Long
    Dim Position As Long
    Dim Count As Long
    Dim Index As Long
    Dim X As Long, Y As Long

    ' dump tile data
    FileName = App.Path & MAP_PATH & "Map" & MapNum & ".maps"

    f = FreeFile
    Open FileName For Binary As #f

    ' get the position of ground data
    Get #f, , Position
    
    ' set the loc to position
    Get #f, Position + NextByte, Count

    MaxGroundParallax = Count
    Erase GroundParallax

    If Count > 0 Then
        ReDim GroundParallax(1 To Count)

        For i = 1 To Count
            Get #f, , Index
            Get #f, , X
            Get #f, , Y
            Get #f, , EncryptedLength

            ReDim Encrypted(0 To EncryptedLength - 1)
            Get #f, , Encrypted
            
            ReDim Decrypted(0 To EncryptedLength - 1)

            Success = Decrypt(ByVal VarPtr(Settings), ByVal VarPtr(Encrypted(0)), EncryptedLength, ByVal VarPtr(Decrypted(0)), ByVal VarPtr(DecryptedLength))

            If Success = False Then
                GoTo Error
            End If
            
            If EncryptedLength <> DecryptedLength Then
                ReDim Preserve Decrypted(0 To DecryptedLength - 1)
            End If

            GroundParallax(Index).Num = Index
            GroundParallax(Index).X = X
            GroundParallax(Index).Y = Y

            Call LoadParallaxTexture(Decrypted, GroundParallax(Index).Texture)
        Next
    End If

    ' get the count of fringe data
    Get #f, , Count

    MaxFringeParallax = Count
    Erase FringeParallax

    If Count > 0 Then
        ReDim FringeParallax(1 To Count)

        For i = 1 To Count
            Get #f, , Index
            Get #f, , X
            Get #f, , Y
            Get #f, , EncryptedLength

            ReDim Encrypted(0 To EncryptedLength - 1)
            Get #f, , Encrypted
            
            ReDim Decrypted(0 To EncryptedLength - 1)

            Success = Decrypt(ByVal VarPtr(Settings), ByVal VarPtr(Encrypted(0)), EncryptedLength, ByVal VarPtr(Decrypted(0)), ByVal VarPtr(DecryptedLength))

            If Success = False Then
                GoTo Error
            End If
            
            If EncryptedLength <> DecryptedLength Then
                ReDim Preserve Decrypted(0 To DecryptedLength - 1)
            End If

            FringeParallax(Index).Num = Index
            FringeParallax(Index).X = X
            FringeParallax(Index).Y = Y

            Call LoadParallaxTexture(Decrypted, FringeParallax(Index).Texture)
        Next
    End If

    Close #f

    Exit Sub

Error:
    MsgBox "Failed to load map"
End Sub

Public Sub LoadParallaxTexture(ByRef Data() As Byte, ByRef Texture As TextureStruct)
    If AryCount(Data) = 0 Then
        Exit Sub
    End If

    Texture.W = ByteToInt(Data(18), Data(19))
    Texture.H = ByteToInt(Data(22), Data(23))
    Texture.Data = Data

    Set Texture.Texture = D3DX.CreateTextureFromFileInMemoryEx(D3DDevice, Data(0), AryCount(Data), Texture.W, Texture.H, D3DX_DEFAULT, 0, D3DFMT_A8R8G8B8, D3DPOOL_MANAGED, D3DX_FILTER_POINT, D3DX_FILTER_POINT, 0, ByVal 0, ByVal 0)
End Sub

Public Sub LoadMapsProperties()
    Dim i As Long, Path As String

    For i = 1 To MaxMaps
        Path = App.Path & MAP_PATH & "Map" & i & ".maps"

        If FileExist(Path) Then
            Call LoadMapProperty(i)
        End If
    Next

End Sub

Public Sub CopyMapProperty(ByVal MapNum As Long)
    If MapNum > 0 And MapNum <= MaxMaps Then
        CurrentMap.MapData = MapPropertyData(MapNum).MapData
        CurrentMap.TileData = MapPropertyData(MapNum).TileData
    End If
End Sub
