Attribute VB_Name = "Wrapper_Data"
Option Explicit

Public Declare Function GetFileHandler Lib "Dragon.Wrapper.dll" Alias "_GetFileHandler@4" (ByVal File As String) As Long
Public Declare Sub CloseFileHandler Lib "Dragon.Wrapper.dll" Alias "_CloseFileHandler@0" ()
Public Declare Function ReadString Lib "Dragon.Wrapper.dll" Alias "_ReadString@4" (ByRef Output As String) As Long
Public Declare Function ReadByte Lib "Dragon.Wrapper.dll" Alias "_ReadByte@0" () As Byte
Public Declare Function ReadBytes Lib "Dragon.Wrapper.dll" Alias "_ReadBytes@8" (ByRef DestIntPtr As Long, ByVal BytesLength As Long) As Long
Public Declare Function ReadInt16 Lib "Dragon.Wrapper.dll" Alias "_ReadInt16@0" () As Integer
Public Declare Function ReadInt32 Lib "Dragon.Wrapper.dll" Alias "_ReadInt32@0" () As Long
Public Declare Function ReadSingle Lib "Dragon.Wrapper.dll" Alias "_ReadSingle@0" () As Single
Public Declare Function ReadBoolean Lib "Dragon.Wrapper.dll" Alias "_ReadBoolean@0" () As Boolean

Public Declare Function Encrypt Lib "Dragon.Wrapper.dll" Alias "_Encrypt@20" (ByVal SettingsLngPtr As Long, ByVal SourceLngPtr As Long, ByVal SourceLength As Long, ByRef DestLngPtr As Long, ByRef DestLengthLngPtr As Long) As Boolean
Public Declare Function Decrypt Lib "Dragon.Wrapper.dll" Alias "_Decrypt@20" (ByVal SettingsLngPtr As Long, ByVal SourceLngPtr As Long, ByVal SourceLength As Long, ByRef DestLngPtr As Long, ByRef DestLengthLngPtr As Long) As Boolean

Public Declare Function Compute Lib "Dragon.Wrapper.dll" Alias "_Compute@8" (ByVal Data As String, ByRef DestLngPtr As Long) As Long
Public Declare Function ConvertToHexadecimal Lib "Dragon.Wrapper.dll" Alias "_ConvertToHexadecimal@12" (ByVal SourceLngPtr As Long, ByVal SourceLength As Long, ByRef Output As String) As Long

Public Declare Function CreateKey Lib "Dragon.Wrapper.dll" Alias "_CreateKey@8" (ByVal Passphrase As String, ByRef DestLngPtr As Long) As Long
Public Declare Function CreateIv Lib "Dragon.Wrapper.dll" Alias "_CreateIv@8" (ByVal Passphrase As String, ByRef DestLngPtr As Long) As Long

Public Declare Function GetCPUId Lib "Dragon.Wrapper.dll" Alias "_GetCPUId@4" (ByRef Output As String) As Long
Public Declare Function GetBIOSId Lib "Dragon.Wrapper.dll" Alias "_GetBIOSId@4" (ByRef Output As String) As Long
Public Declare Function GetDiskId Lib "Dragon.Wrapper.dll" Alias "_GetDiskId@4" (ByRef Output As String) As Long
Public Declare Function GetBoardId Lib "Dragon.Wrapper.dll" Alias "_GetBoardId@4" (ByRef Output As String) As Long
Public Declare Function GetVideoId Lib "Dragon.Wrapper.dll" Alias "_GetVideoId@4" (ByRef Output As String) As Long
Public Declare Function GetMacAddressId Lib "Dragon.Wrapper.dll" Alias "_GetMacAddressId@4" (ByRef Output As String) As Long

Public Declare Sub Cipher Lib "Dragon.Wrapper.dll" Alias "_Cipher@12" (ByVal InstanceIndex As Long, ByVal BufferLngPtr As Long, ByVal Length As Long)
Public Declare Function Decipher Lib "Dragon.Wrapper.dll" Alias "_Decipher@12" (ByVal InstanceIndex As Long, ByVal BufferLngPtr As Long, ByVal Length As Long) As Boolean
Public Declare Sub UpdateKey Lib "Dragon.Wrapper.dll" Alias "_UpdateKey@12" (ByVal InstanceIndex As Long, ByVal BufferLngPtr As Long, ByVal Length As Long)
Public Declare Sub AppendCheckSum Lib "Dragon.Wrapper.dll" Alias "_AppendCheckSum@16" (ByVal InstanceIndex As Long, ByVal BufferLngPtr As Long, ByVal offset As Long, ByVal Length As Long)

' The API uses 16 bytes fixed length key. Do not change.
' A API usa uma chave de tamanho fixa 16 bytes. Não trocar
Public Const AesKeyLength As Long = 16
Public Const CipherKeyLength As Long = 16

Public GameInstanceCipherKey(0 To CipherKeyLength - 1) As Byte
Public ChatInstanceCipherKey(0 To CipherKeyLength - 1) As Byte

' BlowFishCipher Instance Index
Public Const GameInstance As Long = 1
Public Const ChatInstance As Long = 2

' Do not change variable order and types.
' Não trocar a ordem das variáveis e nem os tipos de dados.
Public Type AesSettings
    KeyBitSize As Long
    CipherMode As Long
    PaddingMode As Long
    Key(0 To AesKeyLength - 1) As Byte
    IV(0 To AesKeyLength - 1) As Byte
End Type

Public Enum AesCipherMode
    AesCipherMode_CBC = 1
    AesCipherMode_ECB = 2
    AesCipherMode_OFB = 3
    AesCipherMode_CFB = 4
    AesCipherMode_CTS = 5
End Enum

Public Enum AesBitSize
    AesBitSize_128 = 128
    AesBitSize_192 = 192
    AesBitSize_256 = 256
End Enum

Public Enum AesPaddingMode
    ' No padding is done.
    AesPaddingMode_None = 1
    ' The PKCS #7 padding string consists of a sequence of bytes, each of which is equal to the total number of padding bytes added.
    AesPaddingMode_PKCS7 = 2
    ' The padding string consists of bytes set to zero.
    AesPaddingMode_Zeros = 3
    ' The ANSIX923 padding string consists of a sequence of bytes filled with zeros before the length.
    AesPaddingMode_ANSIX923 = 4
    ' The ISO10126 padding string consists of random data before the length.
    AesPaddingMode_ISO10126 = 5
End Enum

Public Sub UpdateChatInstanceCipherKey()
    ChatInstanceCipherKey(0) = &H6B
    ChatInstanceCipherKey(1) = &H60
    ChatInstanceCipherKey(2) = &HCB
    ChatInstanceCipherKey(3) = &H5B
    ChatInstanceCipherKey(4) = &H82
    ChatInstanceCipherKey(5) = &HCE
    ChatInstanceCipherKey(6) = &H90
    ChatInstanceCipherKey(7) = &HB1
    ChatInstanceCipherKey(8) = &HCC
    ChatInstanceCipherKey(9) = &H2B
    ChatInstanceCipherKey(10) = &H6C
    ChatInstanceCipherKey(11) = &H55
    ChatInstanceCipherKey(12) = &H6C
    ChatInstanceCipherKey(13) = &H6C
    ChatInstanceCipherKey(14) = &H6C
    ChatInstanceCipherKey(15) = &H6C

    Call UpdateKey(ChatInstance, ByVal VarPtr(ChatInstanceCipherKey(0)), CipherKeyLength)
End Sub

Public Sub UpdateGameInstanceCipherKey()
    GameInstanceCipherKey(0) = &H6B
    GameInstanceCipherKey(1) = &H60
    GameInstanceCipherKey(2) = &HCB
    GameInstanceCipherKey(3) = &H5B
    GameInstanceCipherKey(4) = &H82
    GameInstanceCipherKey(5) = &HCE
    GameInstanceCipherKey(6) = &H90
    GameInstanceCipherKey(7) = &HB1
    GameInstanceCipherKey(8) = &HCC
    GameInstanceCipherKey(9) = &H2B
    GameInstanceCipherKey(10) = &H6C
    GameInstanceCipherKey(11) = &H55
    GameInstanceCipherKey(12) = &H6C
    GameInstanceCipherKey(13) = &H6C
    GameInstanceCipherKey(14) = &H6C
    GameInstanceCipherKey(15) = &H6C
    
    Call UpdateKey(GameInstance, ByVal VarPtr(GameInstanceCipherKey(0)), CipherKeyLength)
End Sub



