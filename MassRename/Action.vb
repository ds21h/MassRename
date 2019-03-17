Public Class Action

  Private mAction As Integer
  Private mPosition As Integer
  Private mLength As Integer
  Private mValue As String

  Public Const Insert As Integer = 1
  Private Const cInsert As String = "act_insert"
  Public Const Delete As Integer = 2
  Private Const cDelete As String = "act_delete"

  Public ReadOnly Property xAction As Integer
    Get
      Return mAction
    End Get
  End Property

  Public ReadOnly Property xAktieTekst As String
    Get
      Select Case mAction
        Case Insert
          Return cInsert
        Case Delete
          Return cDelete
        Case Else
          Return ""
      End Select
    End Get
  End Property

  Public Property xStart As Integer
    Get
      Return mPosition
    End Get
    Set(inStart As Integer)
      mPosition = inStart
    End Set
  End Property

  Public Property xLength As Integer
    Get
      Return mLength
    End Get
    Set(inLength As Integer)
      mLength = inLength
    End Set
  End Property

  Public Property xValue As String
    Get
      Return mValue
    End Get
    Set(inValue As String)
      mValue = inValue
    End Set
  End Property

  Public Sub New(pAction As Integer, pStart As Integer, pLength As Integer, pValue As String)
    mAction = pAction
    mPosition = pStart
    mLength = pLength
    mValue = pValue
  End Sub

  Public Function xApply(pInput As String) As String
    Select Case mAction
      Case Insert
        Return sInsert(pInput, mValue, mPosition)
      Case Delete
        Return sDelete(pInput, mPosition, mLength)
      Case Else
        Return pInput
    End Select
  End Function

  Private Function sInsert(pBasis As String, pExtra As String, pPosition As Integer) As String
    Dim lOutput As String

    If pPosition = 0 Then
      lOutput = pExtra & pBasis
    Else
      If pPosition < pBasis.Length Then
        lOutput = Mid(pBasis, 1, pPosition) & pExtra & Mid(pBasis, pPosition + 1, pBasis.Length - pPosition)
      Else
        lOutput = pBasis & pExtra
      End If
    End If

    Return lOutput
  End Function

  Private Function sDelete(pBasis As String, pPosition As Integer, pLength As Integer) As String
    Dim lOutput As String
    Dim lPosition As Integer
    Dim lLength As Integer

    If pPosition > pBasis.Length Then
      lPosition = pBasis.Length
      lLength = 0
    Else
      If pPosition < 0 Then
        lPosition = 0
      Else
        lPosition = pPosition
      End If
      If lPosition + pLength > pBasis.Length Then
        lLength = pBasis.Length - lPosition
      Else
        lLength = pLength
      End If
    End If

    If lPosition = 0 Then
      lOutput = Mid(pBasis, lLength + 1, pBasis.Length - lLength)
    Else
      If lPosition + lLength > pBasis.Length Then
        lOutput = Mid(pBasis, 1, lPosition - 1)
      Else
        lOutput = Mid(pBasis, 1, lPosition) & Mid(pBasis, lPosition + lLength + 1, pBasis.Length - (lPosition + lLength))
      End If
    End If

    Return lOutput
  End Function
End Class
