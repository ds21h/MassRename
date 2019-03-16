Public Class Action

	Private mAktie As Integer
	Private mPositie As Integer
	Private mLengte As Integer
	Private mWaarde As String

	Public Const Invoegen As Integer = 1
	Private Const cInvoegen As String = "Invoegen"
  Public Const Invoegen1 As Integer = 2
  Private Const cInvoegen1 As String = "Invoegen(n)"
  Public Const Verwijderen As Integer = 3
  Private Const cVerwijderen As String = "Verwijderen"
  Public Const Verhoog As Integer = 4
  Private Const cVerhoog As String = "Verhogen"
  Public Const Verlaag As Integer = 5
  Private Const cVerlaag As String = "Verlagen"

  Public ReadOnly Property xAktie As Integer
    Get
      Return mAktie
    End Get
  End Property

  Public ReadOnly Property xAktieTekst As String
		Get
			Select Case mAktie
				Case Invoegen
					Return cInvoegen
        Case Invoegen1
          Return cInvoegen1
        Case Verwijderen
          Return cVerwijderen
        Case Verhoog
          Return cVerhoog
        Case Verlaag
          Return cVerlaag
        Case Else
          Return ""
			End Select
		End Get
	End Property

	Public Property xStart As Integer
		Get
			Return mPositie
		End Get
		Set(inStart As Integer)
			mPositie = inStart
		End Set
	End Property

	Public Property xLengte As Integer
		Get
			Return mLengte
		End Get
		Set(inLengte As Integer)
			mLengte = inLengte
		End Set
	End Property

	Public Property xWaarde As String
		Get
			Return mWaarde
		End Get
		Set(inWaarde As String)
			mWaarde = inWaarde
		End Set
	End Property

	Public Sub New(pAktie As Integer, pStart As Integer, pLengte As Integer, pWaarde As String)
		mAktie = pAktie
		mPositie = pStart
		mLengte = pLengte
		mWaarde = pWaarde
	End Sub

	Public Function xPasToe(pInvoer As String) As String
		Select Case mAktie
			Case Invoegen
				Return sInvoegen(pInvoer, mWaarde, mPositie)
      Case Invoegen1
        Return sInvoegen1(pInvoer, mWaarde, mPositie)
      Case Verwijderen
        Return sVerwijder(pInvoer, mPositie, mLengte)
      Case Verhoog
        Return sVerhoogLaag(pInvoer, mPositie, mLengte, "+")
      Case Verlaag
        Return sVerhoogLaag(pInvoer, mPositie, mLengte, "-")
      Case Else
        Return pInvoer
		End Select
	End Function

  Private Function sInvoegen(pBasis As String, pExtra As String, pPositie As Integer) As String
		Dim lUitvoer As String

		If pPositie = 0 Then
			lUitvoer = pExtra & pBasis
		Else
			If pPositie < pBasis.Length Then
				lUitvoer = Mid(pBasis, 1, pPositie) & pExtra & Mid(pBasis, pPositie + 1, pBasis.Length - pPositie)
			Else
				lUitvoer = pBasis & pExtra
			End If
		End If

		Return lUitvoer
	End Function

  Private Function sInvoegen1(pBasis As String, pExtra As String, pPositie As Integer) As String
    Dim lUitvoer As String
    Dim lTeller As Integer
    Dim lPunt1 As Integer
    Dim lPunt2 As Integer
    Dim lUitvoeren As Boolean

    lPunt1 = 0
    lPunt2 = 0
    For lTeller = pBasis.Length To 1 Step -1
      If Mid(pBasis, lTeller, 1) = "." Then
        If lPunt1 = 0 Then
          lPunt1 = lTeller
        Else
          If lPunt2 = 0 Then
            lPunt2 = lTeller
          Else
            Exit For
          End If
        End If
      End If
    Next

    lUitvoeren = False
    If lPunt2 > 0 Then
      If lPunt1 - lPunt2 = 2 Then
        If Mid(pBasis, lPunt2 + 1, 1) >= "1" And Mid(pBasis, lPunt2 + 1, 1) <= "9" Then
          lUitvoeren = True
        End If
      End If
    End If

    If lUitvoeren Then
      If pPositie = 0 Then
        lUitvoer = pExtra & pBasis
      Else
        If pPositie < pBasis.Length Then
          lUitvoer = Mid(pBasis, 1, pPositie) & pExtra & Mid(pBasis, pPositie + 1, pBasis.Length - pPositie)
        Else
          lUitvoer = pBasis & pExtra
        End If
      End If
    Else
      lUitvoer = pBasis
    End If

    Return lUitvoer
  End Function

  Private Function sVerwijder(pBasis As String, pPositie As Integer, pLengte As Integer) As String
    Dim lUitvoer As String
    Dim lPositie As Integer
    Dim lLengte As Integer

    If pPositie > pBasis.Length Then
      lPositie = pBasis.Length
      lLengte = 0
    Else
      If pPositie < 0 Then
        lPositie = 0
      Else
        lPositie = pPositie
      End If
      If lPositie + pLengte > pBasis.Length Then
        lLengte = pBasis.Length - lPositie
      Else
        lLengte = pLengte
      End If
    End If

    If lPositie = 0 Then
      lUitvoer = Mid(pBasis, lLengte + 1, pBasis.Length - lLengte)
    Else
      If lPositie + lLengte > pBasis.Length Then
        lUitvoer = Mid(pBasis, 1, lPositie - 1)
      Else
        lUitvoer = Mid(pBasis, 1, lPositie) & Mid(pBasis, lPositie + lLengte + 1, pBasis.Length - (lPositie + lLengte))
      End If
    End If

    Return lUitvoer
  End Function

  Private Function sVerhoogLaag(pBasis As String, pPositie As Integer, pLengte As Integer, pSuffix As String) As String
    Const cMaxLengte As Integer = 6
    Const cFormatString As String = "000000"
    Dim lInPref As String
    Dim lInWerk As String
    Dim lInWerkHaakje As String
    Dim lInWerkInt As Integer
    Dim lInWerkFormat As String
    Dim lInSuf As String
    Dim lStart As Integer
    Dim lEind As Integer
    Dim lLengte As Integer
    Dim lHaakjes As Boolean
    Dim lTeller As Integer

    If pPositie <= 0 Then
      lStart = 0
      lLengte = pLengte
    Else
      If pPositie >= pBasis.Length Then
        lStart = pBasis.Length
        lLengte = 0
      Else
        lStart = pPositie
        lLengte = pLengte
      End If
    End If
    If lLengte > cMaxLengte Then
      lStart = lStart + (lLengte - cMaxLengte)
      lLengte = cMaxLengte
    End If

    If Mid(pBasis, lStart + 1, 1) = "(" Then
      lHaakjes = True
      lEind = pBasis.Length
      For lTeller = lStart + 2 To pBasis.Length
        If Mid(pBasis, lTeller, 1) = ")" Then
          lEind = lTeller
          Exit For
        Else
          If Mid(pBasis, lTeller, 1) = "." Then
            lEind = lTeller - 1
            Exit For
          End If
        End If
      Next
      lLengte = lEind - lStart
    Else
      lHaakjes = False
    End If
    If lStart = 0 Then
      lInPref = ""
    Else
      lInPref = Mid(pBasis, 1, lStart)
    End If
    lInWerk = Mid(pBasis, lStart + 1, lLengte)
    If lStart + lLengte < pBasis.Length Then
      lInSuf = Mid(pBasis, lStart + lLengte + 1)
    Else
      lInSuf = ""
    End If
    If lStart + lLengte > pBasis.Length Then
      lLengte = pBasis.Length - lStart
    End If

    If lHaakjes Then
      If lLengte > 2 Then
        lInWerkHaakje = Mid(lInWerk, 2, lInWerk.Length - 2)
        If IsNumeric(lInWerkHaakje) Then
          lInWerkInt = CInt(lInWerkHaakje)
          If pSuffix = "+" Then
            lInWerkInt = lInWerkInt + 1
          Else
            lInWerkInt = lInWerkInt - 1
          End If
          lInWerkFormat = Format(lInWerkInt, "##########0")
          lInWerk = pSuffix & "(" & lInWerkFormat & ")"
        End If
      End If
    Else
      If IsNumeric(lInWerk) Then
        lInWerkInt = CInt(lInWerk)
        If pSuffix = "+" Then
          lInWerkInt = lInWerkInt + 1
        Else
          lInWerkInt = lInWerkInt - 1
        End If
        lInWerkFormat = Format(lInWerkInt, cFormatString)
        lInWerk = Mid(lInWerkFormat, (lInWerkFormat.Length - lLengte) + 1) & pSuffix
      End If
    End If

    Return lInPref & lInWerk & lInSuf
  End Function

End Class
