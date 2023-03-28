Attribute VB_Name = "Textures_Data"
' Texture paths
Option Explicit

Public Const Path_Font As String = "\Data Files\Graphics\Fonts\"

Private Const Path_Face As String = "\Data Files\Graphics\Face.pak"
Private Const Path_Fog As String = "\Data Files\Graphics\Fog.pak"
Private Const Path_Item As String = "\Data Files\Graphics\IconItem.pak"
Private Const Path_Anim As String = "\Data Files\Graphics\Animation.pak"
Private Const Path_SpellIcon As String = "\Data Files\Graphics\IconSkill.pak"
Private Const Path_Design As String = "\Data Files\Graphics\TextureDesign.pak"
Private Const Path_Gradient As String = "\Data Files\Graphics\TextureGradient.pak"
Private Const Path_GUI As String = "\Data Files\Graphics\TextureInterface.pak"
Private Const Path_Misc As String = "\Data Files\Graphics\TextureMisc.pak"
Private Const Path_Surface As String = "\Data Files\Graphics\Surface.pak"
Private Const Path_Boxes As String = "\Data Files\Graphics\Boxes.pak"

Private Const DefaultText As String = "This is a text!"
Private Const SecurityText As String = "This is a complement"

' Texture wrapper
Public Tex_Anim() As Long
Public Tex_Item() As Long
Public Tex_Face() As Long
Public Tex_Spellicon() As Long
Attribute Tex_Spellicon.VB_VarUserMemId = 1073741828
Public Tex_Fog() As Long
Public Tex_GUI() As Long
Public TextureDesign() As Long
Public Tex_Gradient() As Long
Public Tex_Surface() As Long
Public Tex_Misc() As Long
Public Tex_Boxes() As Long

Public Tex_Bars As Long, Tex_Fader As Long, Tex_Blank As Long
Attribute Tex_Bars.VB_VarUserMemId = 1073741834

' Texture count
Public Count_Anim As Long
Attribute Count_Anim.VB_VarUserMemId = 1073741841
Public Count_GUI As Long
Public Count_Design As Long
Public Count_Gradient As Long
Public Count_Face As Long
Public Count_Item As Long
Attribute Count_Item.VB_VarUserMemId = 1073741846
Public Count_Spellicon As Long
Public Count_Fog As Long
Public Count_Surface As Long
Public Count_Misc As Long
Public Count_Boxes As Long

Public Sub LoadTextures()
' Arrays
    Tex_Anim = LoadTexturePack(Count_Anim, App.Path & Path_Anim)
    Tex_Spellicon = LoadTexturePack(Count_Spellicon, App.Path & Path_SpellIcon)
    Tex_Item = LoadTexturePack(Count_Item, App.Path & Path_Item)
    Tex_Fog = LoadTexturePack(Count_Fog, App.Path & Path_Fog)
    Tex_Face = LoadTexturePack(Count_Face, App.Path & Path_Face)
    TextureDesign = LoadTexturePack(Count_Design, App.Path & Path_Design)
    Tex_Gradient = LoadTexturePack(Count_Gradient, App.Path & Path_Gradient)
    Tex_GUI = LoadTexturePack(Count_GUI, App.Path & Path_GUI)
    Tex_Surface = LoadTexturePack(Count_Surface, App.Path & Path_Surface)
    Tex_Misc = LoadTexturePack(Count_Misc, App.Path & Path_Misc)
    Tex_Boxes = LoadTexturePack(Count_Misc, App.Path & Path_Boxes)

    Tex_Bars = Tex_Misc(TextureMisc.TextureMisc_Bars)
    Tex_Fader = Tex_Misc(TextureMisc.TextureMisc_Fader)
    Tex_Blank = Tex_Misc(TextureMisc.TextureMisc_Blank)
End Sub

Public Function LoadTexturePack(ByRef Counter As Long, ByRef Path As String) As Long()
    Dim Textures() As Long
    Dim FileCount As Long
    Dim Password As String
    Dim Index As Long
    Dim i As Long

    If Not FileExist(Path) Then
        Call MsgBox("File '" & Path & "' not found.")

        Exit Function
    End If

    Password = "dwHandle" & SecurityText

    Index = GetFileHandler(Path)

    If Index = 0 Then
        If CheckFilePassword(Password) Then
            Dim Settings As AesSettings

            Settings.CipherMode = AesCipherMode.AesCipherMode_CBC
            Settings.PaddingMode = AesPaddingMode.AesPaddingMode_PKCS7
            Settings.KeyBitSize = AesBitSize.AesBitSize_128

            CreateKey Password, ByVal VarPtr(Settings.Key(0))
            CreateIv Password, ByVal VarPtr(Settings.IV(0))

            Dim Success As Boolean
            Dim Name As String
            Dim Extension As String
            Dim Width As Long
            Dim Height As Long
            Dim Length As Long
            Dim Encrypted() As Byte
            Dim EncryptedLength As Long
            Dim Decrypted() As Byte
            Dim DecryptedLength As Long

            Counter = ReadInt32()

            ReDim Textures(0 To Counter)

            For i = 1 To Counter
                Name = String(1024, vbNullChar)
                Extension = String(1024, vbNullChar)

                Call ReadString(Name)
                Call ReadString(Extension)

                Name = Replace$(Name, vbNullChar, vbNullString)
                Extension = Replace$(Extension, vbNullChar, vbNullString)

                Width = ReadInt32()
                Height = ReadInt32()

                EncryptedLength = ReadInt32()
                ReDim Encrypted(0 To EncryptedLength - 1)

                Call ReadBytes(ByVal VarPtr(Encrypted(0)), EncryptedLength)

                ReDim Decrypted(0 To EncryptedLength - 1)

                Success = Decrypt(ByVal VarPtr(Settings), ByVal VarPtr(Encrypted(0)), EncryptedLength, ByVal VarPtr(Decrypted(0)), ByVal VarPtr(DecryptedLength))

                If Success Then
                    ReDim Preserve Decrypted(0 To DecryptedLength)
                    Textures(i) = LoadTexture(Decrypted)
                End If

                DoEvents
            Next

            LoadTexturePack = Textures
        Else
            MsgBox "Invalid File Password: " & vbNewLine & Path
        End If
    End If

    Call CloseFileHandler

End Function

Public Function LoadTexture(ByRef Data() As Byte, Optional ByVal DontReuse As Boolean) As Long
    Dim i As Long

    If AryCount(Data) = 0 Then
        Exit Function
    End If

    mTextures = mTextures + 1
    LoadTexture = mTextures
    ReDim Preserve mTexture(1 To mTextures) As TextureStruct
    mTexture(mTextures).W = ByteToInt(Data(18), Data(19))
    mTexture(mTextures).H = ByteToInt(Data(22), Data(23))
    mTexture(mTextures).Data = Data

    Set mTexture(mTextures).Texture = D3DX.CreateTextureFromFileInMemoryEx(D3DDevice, Data(0), AryCount(Data), mTexture(mTextures).W, mTexture(mTextures).H, D3DX_DEFAULT, 0, D3DFMT_A8R8G8B8, D3DPOOL_MANAGED, D3DX_FILTER_POINT, D3DX_FILTER_POINT, 0, ByVal 0, ByVal 0)
End Function

Private Function CheckFilePassword(ByVal Password As String) As Boolean
    Dim Settings As AesSettings
    Dim Encrypted() As Byte
    Dim EncryptedLength As Long
    Dim Decrypted() As Byte
    Dim DecryptedLength As Long
    Dim Success As Boolean
    Dim i As Long

    EncryptedLength = ReadInt32()

    ReDim Encrypted(0 To EncryptedLength - 1)
    
    Call ReadBytes(ByVal VarPtr(Encrypted(0)), EncryptedLength)

    Settings.CipherMode = AesCipherMode.AesCipherMode_CBC
    Settings.PaddingMode = AesPaddingMode.AesPaddingMode_PKCS7
    Settings.KeyBitSize = AesBitSize.AesBitSize_128

    CreateKey Password, ByVal VarPtr(Settings.Key(0))
    CreateIv Password, ByVal VarPtr(Settings.IV(0))

    ReDim Decrypted(1024)

    Success = Decrypt(ByVal VarPtr(Settings), ByVal VarPtr(Encrypted(0)), EncryptedLength, ByVal VarPtr(Decrypted(0)), ByVal VarPtr(DecryptedLength))

    If Success Then
        Dim ComputedHash(1024) As Byte
        Dim ComputedHashLength As Long

        ComputedHashLength = Compute(DefaultText, ByVal VarPtr(ComputedHash(0)))

        If UBound(ComputedHash) <> UBound(Decrypted) Then
            CheckFilePassword = False
        End If

        If CheckEquality(ComputedHash, ComputedHashLength, Decrypted, DecryptedLength) Then
            CheckFilePassword = True
        End If
    End If

End Function

Public Function CheckEquality(ByRef First() As Byte, ByVal FirstLength As Long, ByRef Second() As Byte, ByVal SecondLength As Long) As Boolean
    Dim i As Long
        
    CheckEquality = False

    If FirstLength <> SecondLength Then
        Exit Function
    End If
    
    For i = 0 To FirstLength
       If First(i) <> Second(i) Then
            Exit Function
        End If
    Next
    
    CheckEquality = True

End Function

Public Function LoadTextureFiles(ByRef Counter As Long, ByVal Path As String) As Long()
    Dim Texture() As Long
    Dim i As Long

    Counter = 1

    Do While Dir$(Path & Counter + 1 & ".png") <> vbNullString
        Counter = Counter + 1
    Loop

    ReDim Texture(0 To Counter)

    For i = 1 To Counter
        Texture(i) = LoadTextureFile(Path & i & ".png")
        DoEvents
    Next

    LoadTextureFiles = Texture
End Function

Public Function LoadTextureFile(ByVal Path As String, Optional ByVal DontReuse As Boolean) As Long
    Dim Data() As Byte
    Dim f As Long

    If Dir$(Path) = vbNullString Then
        Call MsgBox("""" & Path & """ could not be found.")
        End
    End If

    f = FreeFile
    Open Path For Binary As #f
    ReDim Data(0 To LOF(f) - 1)
    Get #f, , Data
    Close #f

    LoadTextureFile = LoadTexture(Data, DontReuse)
End Function

