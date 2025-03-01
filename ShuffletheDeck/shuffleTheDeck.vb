'Tim Rossiter
'RCET 2265
'Spring 2025
'Shuffle The Deck
'https://github.com/rc-earnest/ShuffletheDeck.git
Option Strict On
Option Explicit On
Module shuffleTheDeck
    Sub Main()
        Dim userInput As String
        Dim _lastBall(1) As Integer

        Do
            Console.Clear()
            ' DisplayBoard()
            _lastBall = lastCard()
            Console.WriteLine($"The card drawn was the {FormatCardNumber(_lastBall(0), 0)} of {FormatCardSuit(_lastBall(1))}")
            Console.WriteLine($"Cards left in deck: {52 - cardCounter}")
            Console.WriteLine("Enter what you would like to do:")
            Console.WriteLine("q to quit")
            Console.WriteLine("d to draw a bingo ball")
            Console.WriteLine("c to clear game")
            userInput = Console.ReadLine()
            Select Case userInput
                Case "d"
                    DrawCard()
                Case "c"
                    DeckTracker(0, 0,, True)
                    DrawCard(True)
                Case Else
                    'pass
            End Select

        Loop Until userInput = "q"
    End Sub

    ''' <summary>
    ''' Iterates through the traker array and displays bingo board to the console
    ''' </summary>

    Sub DisplayBoard()
        Dim displayString As String
        Dim heading() As String = {"S", "H", "D", "C"}
        Dim tracker(,) As Boolean = DeckTracker(0, 0)
        Dim columnWidth As Integer = 5

        For Each letter In heading
            Console.Write(letter.PadLeft((columnWidth \ 2) + 1).PadRight(columnWidth))
        Next

        Console.WriteLine()
        Console.WriteLine(StrDup(columnWidth * 5, "_"))

        For currentNumber = 0 To 12
            For currentLetter = 0 To 3
                If tracker(currentNumber, currentLetter) Then
                    displayString = $"{FormatCardNumber(currentNumber, currentLetter)} |" 'display for drawn balls
                Else
                    displayString = "|" 'display for not drawn balls
                End If
                displayString = displayString.PadLeft(columnWidth)
                Console.Write(displayString)

            Next
            Console.WriteLine()
        Next
    End Sub
    ''' <summary>
    ''' Gets two random numbers until they haven't been drawn then initializes bingo tracker to write those numbers to our tracker.
    ''' </summary>
    ''' <param name="clearCount">This clears the count of</param>
    ''' 
    Public cardCounter As Integer
    Sub DrawCard(Optional clearCount As Boolean = False)
        Dim temp(,) As Boolean = DeckTracker(0, 0) 'create a local copy of ball tracker array
        Dim currentBallNumber As Integer
        Dim currentBallLetter As Integer

        If clearCount Then
            cardCounter = 0
        Else
            'loop until the current random ball has not already been marked as drawn
            Do
                currentBallNumber = randomNumberBetween(0, 12) 'get the row
                currentBallLetter = randomNumberBetween(0, 3) 'get the column

            Loop Until temp(currentBallNumber, currentBallLetter) = False Or cardCounter >= 52

            'mark current ball as being drawn, updates the display

            DeckTracker(currentBallNumber, currentBallLetter, True)
            cardCounter += 1
        End If
        If cardCounter = 53 Then
            DrawCard(True)
            DeckTracker(0, 0,, True)
        End If
        lastCard(currentBallNumber, currentBallLetter)
    End Sub
    Function lastCard(Optional cardNumber As Integer = -1, Optional cardLetter As Integer = -1) As Integer()
        Static _lastCard(1) As Integer

        If cardNumber <> -1 Then
            _lastCard(0) = cardNumber
            _lastCard(1) = cardLetter
        End If

        Return _lastCard
    End Function
    ''' <summary>
    ''' Contains a persistant array that tracks all possible bingo balls 
    ''' and whether they have been drawn during the current game.
    ''' </summary>
    ''' <param name="cardNumber"></param>
    ''' <param name="cardLetter"></param>
    ''' <param name="clear"></param>
    ''' <returns>Current Tracking Array</returns>

    Function DeckTracker(cardNumber As Integer,
                          cardLetter As Integer,
                          Optional Update As Boolean = False,
                          Optional clear As Boolean = False) _
                          As Boolean(,)
        Static _deckTracker(12, 3) As Boolean

        If clear Then
            ReDim _deckTracker(12, 3)
        ElseIf Update Then
            _deckTracker(cardNumber, cardLetter) = True
        End If

        Return _deckTracker
    End Function

    Function FormatCardNumber(cardNumber As Integer, cardLetter As Integer) As String
        Dim _cardNumber As String
        cardNumber += 1
        _cardNumber = Str((cardNumber))
        Select Case cardNumber
            Case 1
                _cardNumber = "A"
            Case 11
                _cardNumber = "J"
            Case 12
                _cardNumber = "Q"
            Case 13
                _cardNumber = "K"
        End Select

        Return _cardNumber
    End Function

    Function FormatCardSuit(cardSuit As Integer) As String
        Dim _formatCardSuit As String
        Select Case cardSuit
            Case 0
                _formatCardSuit = "Spades"
            Case 1
                _formatCardSuit = "Hearts"
            Case 2
                _formatCardSuit = "Diamonds"
            Case 3
                _formatCardSuit = "Clubs"
        End Select
        Return _formatCardSuit
    End Function

    Function randomNumberBetween(min As Integer, max As Integer) As Integer
        Randomize()
        Dim randomNumber As Single
        randomNumber = Rnd()
        randomNumber *= max - min + 1
        randomNumber += min - 1
        Return CInt(Math.Ceiling(randomNumber))
    End Function


End Module
