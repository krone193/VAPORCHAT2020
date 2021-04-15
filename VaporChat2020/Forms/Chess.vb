Public Class Chess
	'--- V A P O R C H E S S | Declarations --------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
#Const VAPORCHAT_SWVER = "0.0.0.1"


	Const SQUAREDIM As Byte = 55

	ReadOnly BOARD_COLOUR_LIGHT As Color = Color.LavenderBlush
	ReadOnly BOARD_COLOUR_DARK As Color = Color.PaleVioletRed

	ReadOnly WPAWN As Image = Image.FromFile("Resources/Chess/wPawn.png")
	ReadOnly WBISHOP As Image = Image.FromFile("Resources/Chess/wBishop.png")
	ReadOnly WKNIGHT As Image = Image.FromFile("Resources/Chess/wKnight.png")
	ReadOnly WROOK As Image = Image.FromFile("Resources/Chess/wRook.png")
	ReadOnly WQUEEN As Image = Image.FromFile("Resources/Chess/wQueen.png")
	ReadOnly WKING As Image = Image.FromFile("Resources/Chess/wKing.png")

	ReadOnly BPAWN As Image = Image.FromFile("Resources/Chess/bPawn.png")
	ReadOnly BBISHOP As Image = Image.FromFile("Resources/Chess/bBishop.png")
	ReadOnly BKNIGHT As Image = Image.FromFile("Resources/Chess/bKnight.png")
	ReadOnly BROOK As Image = Image.FromFile("Resources/Chess/bRook.png")
	ReadOnly BQUEEN As Image = Image.FromFile("Resources/Chess/bQueen.png")
	ReadOnly BKING As Image = Image.FromFile("Resources/Chess/bKing.png")

	Enum cl
		a
		b
		c
		d
		e
		f
		g
		h
		columns
	End Enum
	Enum rw
		r1
		r2
		r3
		r4
		r5
		r6
		r7
		r8
		rows
	End Enum

	Enum Types
		Pawn
		Bishop
		Knight
		Rook
		Queen
		King
		NofTypes
	End Enum

	Enum Colors
		White
		Black
	End Enum

	Structure Piece
		Dim Alive As Boolean
		Dim First As Boolean
		Dim Color As Colors
		Dim Type As Types
		Dim Position As Panel
	End Structure

	Dim StartPiecePosition As Panel = Nothing
	Dim StartBoardColor As Color
	Dim ClickOn As Boolean = False

	ReadOnly Board(cl.columns, rw.rows) As Panel
	Dim AColumn(rw.rows) As Panel
	Dim BColumn(rw.rows) As Panel
	Dim CColumn(rw.rows) As Panel
	Dim DColumn(rw.rows) As Panel
	Dim EColumn(rw.rows) As Panel
	Dim FColumn(rw.rows) As Panel
	Dim GColumn(rw.rows) As Panel
	Dim HColumn(rw.rows) As Panel
	Dim Pieces As New List(Of Piece)
	Dim MovingPiece As New Piece
	Dim LandingPiece As New Piece

	Private Sub RotateBoard(ByVal black As Boolean)
		If black = False Then
			For i As rw = rw.r1 To rw.r8
				AColumn(i).Location = New Point(54 * cl.a, i * 54)
				BColumn(i).Location = New Point(54 * cl.b, i * 54)
				CColumn(i).Location = New Point(54 * cl.c, i * 54)
				DColumn(i).Location = New Point(54 * cl.d, i * 54)
				EColumn(i).Location = New Point(54 * cl.e, i * 54)
				FColumn(i).Location = New Point(54 * cl.f, i * 54)
				GColumn(i).Location = New Point(54 * cl.g, i * 54)
				HColumn(i).Location = New Point(54 * cl.h, i * 54)
			Next
			LblC1.Text = "   a"
			LblC2.Text = "   b"
			LblC3.Text = "   c"
			LblC4.Text = "   d"
			LblC5.Text = "   e"
			LblC6.Text = "   f"
			LblC7.Text = "   g"
			LblC8.Text = "   h"
			LblR1.Text = "1"
			LblR2.Text = "2"
			LblR3.Text = "3"
			LblR4.Text = "4"
			LblR5.Text = "5"
			LblR6.Text = "6"
			LblR7.Text = "7"
			LblR8.Text = "8"
		Else
			For i As rw = rw.r1 To rw.r8
				AColumn(i).Location = New Point(54 * cl.h, i * 54)
				BColumn(i).Location = New Point(54 * cl.g, i * 54)
				CColumn(i).Location = New Point(54 * cl.f, i * 54)
				DColumn(i).Location = New Point(54 * cl.e, i * 54)
				EColumn(i).Location = New Point(54 * cl.d, i * 54)
				FColumn(i).Location = New Point(54 * cl.c, i * 54)
				GColumn(i).Location = New Point(54 * cl.b, i * 54)
				HColumn(i).Location = New Point(54 * cl.a, i * 54)
			Next
			LblC1.Text = "   h"
			LblC2.Text = "   g"
			LblC3.Text = "   f"
			LblC4.Text = "   e"
			LblC5.Text = "   d"
			LblC6.Text = "   c"
			LblC7.Text = "   b"
			LblC8.Text = "   a"
			LblR1.Text = "8"
			LblR2.Text = "7"
			LblR3.Text = "6"
			LblR4.Text = "5"
			LblR5.Text = "4"
			LblR6.Text = "3"
			LblR7.Text = "2"
			LblR8.Text = "1"
		End If
	End Sub

	Private Sub Chess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Dim InitPiece As Piece

		' Init | Assign columns' panels
		AColumn = {a1, a2, a3, a4, a5, a6, a7, a8}
		BColumn = {b1, b2, b3, b4, b5, b6, b7, b8}
		CColumn = {c1, c2, c3, c4, c5, c6, c7, c8}
		DColumn = {d1, d2, d3, d4, d5, d6, d7, d8}
		EColumn = {e1, e2, e3, e4, e5, e6, e7, e8}
		FColumn = {f1, f2, f3, f4, f5, f6, f7, f8}
		GColumn = {g1, g2, g3, g4, g5, g6, g7, g8}
		HColumn = {h1, h2, h3, h4, h5, h6, h7, h8}

		' Init | Assign board matrix
		For i As rw = rw.r1 To rw.r8
			Board(cl.a, i) = AColumn(i)
			Board(cl.b, i) = BColumn(i)
			Board(cl.c, i) = CColumn(i)
			Board(cl.d, i) = DColumn(i)
			Board(cl.e, i) = EColumn(i)
			Board(cl.f, i) = FColumn(i)
			Board(cl.g, i) = GColumn(i)
			Board(cl.h, i) = HColumn(i)
		Next

		' Init | Place pieces
		' PAWS: place white paws
		InitPiece.Alive = True
		InitPiece.First = True
		InitPiece.Color = Colors.White
		InitPiece.Type = Types.Pawn
		For i As cl = cl.a To cl.h
			InitPiece.Position = Board(i, rw.r2)
			InitPiece.Position.BackgroundImage = WPAWN
			Pieces.Add(InitPiece)
		Next
		' PAWS: place black paws
		InitPiece.Color = Colors.Black
		For i As cl = cl.a To cl.h
			InitPiece.Position = Board(i, rw.r7)
			InitPiece.Position.BackgroundImage = BPAWN
			Pieces.Add(InitPiece)
		Next

		' KNIGHTS: place white knights
		InitPiece.Color = Colors.White
		InitPiece.Type = Types.Knight
		InitPiece.Position = Board(cl.b, rw.r1)
		InitPiece.Position.BackgroundImage = WKNIGHT
		Pieces.Add(InitPiece)
		InitPiece.Position = Board(cl.g, rw.r1)
		InitPiece.Position.BackgroundImage = WKNIGHT
		Pieces.Add(InitPiece)
		' KNIGHTS: place black knights
		InitPiece.Color = Colors.Black
		InitPiece.Position = Board(cl.b, rw.r8)
		InitPiece.Position.BackgroundImage = BKNIGHT
		Pieces.Add(InitPiece)
		InitPiece.Position = Board(cl.g, rw.r8)
		InitPiece.Position.BackgroundImage = BKNIGHT
		Pieces.Add(InitPiece)

		' BISHOPS: place white bishops
		InitPiece.Color = Colors.White
		InitPiece.Type = Types.Bishop
		InitPiece.Position = Board(cl.c, rw.r1)
		InitPiece.Position.BackgroundImage = WBISHOP
		Pieces.Add(InitPiece)
		InitPiece.Position = Board(cl.f, rw.r1)
		InitPiece.Position.BackgroundImage = WBISHOP
		Pieces.Add(InitPiece)
		' BISHOPS: place black bishops
		InitPiece.Color = Colors.Black
		InitPiece.Position = Board(cl.c, rw.r8)
		InitPiece.Position.BackgroundImage = BBISHOP
		Pieces.Add(InitPiece)
		InitPiece.Position = Board(cl.f, rw.r8)
		InitPiece.Position.BackgroundImage = BBISHOP
		Pieces.Add(InitPiece)

		' ROOKS: place white rooks
		InitPiece.Color = Colors.White
		InitPiece.Type = Types.Rook
		InitPiece.Position = Board(cl.a, rw.r1)
		InitPiece.Position.BackgroundImage = WROOK
		Pieces.Add(InitPiece)
		InitPiece.Position = Board(cl.h, rw.r1)
		InitPiece.Position.BackgroundImage = WROOK
		Pieces.Add(InitPiece)
		' ROOKS: place black rooks
		InitPiece.Color = Colors.Black
		InitPiece.Position = Board(cl.a, rw.r8)
		InitPiece.Position.BackgroundImage = BROOK
		Pieces.Add(InitPiece)
		InitPiece.Position = Board(cl.h, rw.r8)
		InitPiece.Position.BackgroundImage = BROOK
		Pieces.Add(InitPiece)

		' QUEEN: place white queen
		InitPiece.Color = Colors.White
		InitPiece.Type = Types.Queen
		InitPiece.Position = Board(cl.d, rw.r1)
		InitPiece.Position.BackgroundImage = WQUEEN
		Pieces.Add(InitPiece)
		' QUEEN: place black queen
		InitPiece.Color = Colors.Black
		InitPiece.Position = Board(cl.d, rw.r8)
		InitPiece.Position.BackgroundImage = BQUEEN
		Pieces.Add(InitPiece)

		' KING: place white king
		InitPiece.Color = Colors.White
		InitPiece.Type = Types.King
		InitPiece.Position = Board(cl.e, rw.r1)
		InitPiece.Position.BackgroundImage = WKING
		Pieces.Add(InitPiece)
		' KING: place black king
		InitPiece.Color = Colors.Black
		InitPiece.Position = Board(cl.e, rw.r8)
		InitPiece.Position.BackgroundImage = BKING
		Pieces.Add(InitPiece)
	End Sub

	Private Function IfPresentGetPiece(ByRef position As Panel, ByRef dest As Piece) As Boolean
		Dim i As Byte = 0
		For Each piece As Piece In Pieces
			If position Is piece.Position Then
				If piece.Alive Then
					dest = Pieces(i)
					Return True
				End If
			Else
				i += 1
			End If
		Next
		Return False
	End Function

	Private Sub MovePiece(ByRef destination)
		Dim MovedPiece As Piece = MovingPiece
		MovedPiece.First = False
		MovedPiece.Position = destination
		MovedPiece.Position.BackgroundImage = MovingPiece.Position.BackgroundImage
		MovingPiece.Position.BackgroundImage = Nothing
		Pieces.Remove(MovingPiece)
		Pieces.Add(MovedPiece)
		'StartPiecePosition.BackgroundImage = Nothing
	End Sub

	Private Sub CapturePiece(ByRef captured)
		Pieces.Remove(captured)
	End Sub

	Private Sub Chess_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

	End Sub


	Private Sub Chess_MouseClick(sender As Object, e As MouseEventArgs) Handles a1.MouseClick, a2.MouseClick, a3.MouseClick, a4.MouseClick, a5.MouseClick, a6.MouseClick, a7.MouseClick, a8.MouseClick,
		b1.MouseClick, b2.MouseClick, b3.MouseClick, b4.MouseClick, b5.MouseClick, b6.MouseClick, b7.MouseClick, b8.MouseClick,
		c1.MouseClick, c2.MouseClick, c3.MouseClick, c4.MouseClick, c5.MouseClick, c6.MouseClick, c7.MouseClick, c8.MouseClick,
		d1.MouseClick, d2.MouseClick, d3.MouseClick, d4.MouseClick, d5.MouseClick, d6.MouseClick, d7.MouseClick, d8.MouseClick,
		e1.MouseClick, e2.MouseClick, e3.MouseClick, e4.MouseClick, e5.MouseClick, e6.MouseClick, e7.MouseClick, e8.MouseClick,
		f1.MouseClick, f2.MouseClick, f3.MouseClick, f4.MouseClick, f5.MouseClick, f6.MouseClick, f7.MouseClick, f8.MouseClick,
		g1.MouseClick, g2.MouseClick, g3.MouseClick, g4.MouseClick, g5.MouseClick, g6.MouseClick, g7.MouseClick, g8.MouseClick,
		h1.MouseClick, h2.MouseClick, h3.MouseClick, h4.MouseClick, h5.MouseClick, h6.MouseClick, h7.MouseClick, h8.MouseClick

		LblDebug1.Text = sender.Name.ToString()

		If e.Button = MouseButtons.Left Then
			' First click: select a piece
			If ClickOn = False Then
				If IfPresentGetPiece(sender, MovingPiece) Then
					StartPiecePosition = sender
					StartBoardColor = StartPiecePosition.BackColor
					sender.BackColor = Color.Teal
					ClickOn = True
					LblDebug2.Text = MovingPiece.Position.Name.ToString()
				End If

			Else
				' Second click: move a piece
				ClickOn = False
				StartPiecePosition.BackColor = StartBoardColor
				If sender IsNot StartPiecePosition Then
					' Confirm move
					If IfPresentGetPiece(sender, LandingPiece) Then
						If MovingPiece.Color = LandingPiece.Color Then
							' Cannot auto-capture pieces
						Else
							' Capture a piece
							CapturePiece(LandingPiece)
							MovePiece(sender)
						End If
					Else
						MovePiece(sender)
					End If
				Else
					' Cancel move
					StartPiecePosition = PnlGridBoard
				End If

			End If
		End If
	End Sub

	Private Sub ChbplayAsWhite_CheckedChanged(sender As Object, e As EventArgs) Handles ChbplayAsBlack.CheckedChanged
		RotateBoard(sender.Checked)
		ChbplayAsBlack.Enabled = False
	End Sub
End Class