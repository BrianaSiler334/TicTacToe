namespace TicTacToe {
    internal class Program {

        static void Main(string[] args) {

            int playAgain = 0;
            int results = 0;
            int p1wins = 0;
            int p2wins = 0;
            int draws = 0;

            do {
                //PLAY GAME
                results = Game();

                if (results == 1) p1wins++;
                else if (results == 2) p2wins++;
                else draws++;

                Console.WriteLine($"\nPlayer1: {p1wins}\tDraw:{draws}\t Player2:{p2wins}");

                playAgain = IntInputVal("\n\nPress 1 to play again. Press 0 to quit.");

                if (playAgain == 0) {
                    break;
                }// END IF

            } while (playAgain == 1);

        }//END MAIN

        static int Game() {

            int result = 0;
            bool keepPlaying = true;
            int player = 0;
            int whoseTurn = 1;

            //CREATE BOARD
            string[,] board = new string[,] {

                {"*", "*", "*" },
                {"*", "*", "*" },
                {"*", "*", "*" },
            };

            //GAME LOOP
            while (keepPlaying == true) {

                Console.Clear();

                //PRINT BOARD
                DrawBoard(board);

                //IF TURN IS EVEN = P2
                if (whoseTurn % 2 == 0) {
                    player = 2;
                    //ELSE, P1
                } else {
                    player = 1;
                }

                //PLAY TURN PER 'PLAYER'
                int winLoseDraw = Turn(board, player);

                //INCREMENT WHOSETURN TO SWITCH PLAYERS
                whoseTurn++;

                Console.Clear();

                DrawBoard(board);

                //IF WIN
                if (winLoseDraw == 1) {
                    Console.WriteLine($"Player {player} WINS!!");
                    result = player;
                    keepPlaying = false;

                    //IF DRAW
                } else if (winLoseDraw == 0) {
                    Console.WriteLine($"It's a DRAW!!");
                    result = 0;
                    keepPlaying = false;

                    //IF NEITHER(CONTINUE)
                } else {
                    keepPlaying = true;
                }

            }// end while
            return result;

        }//end main

        static int Turn(string[,] board, int player) {

            string icon = "";
            bool canPlace = false;
            int x, y;

            // IF PLAYER SET ICON
            if (player == 1) {
                icon = "X";
            } else {
                icon = "O";
            }

            //INPUT VAL
            do {

                //GET PLAYER CHOICE(x,y coordinates)
                (x, y) = GetPlayerChoices(player);

                canPlace = PlaceMarker(board, "*", x, y);

                //IF CANPLACE IS TRUE
                if (canPlace) {

                    //PLACE ICON
                    board[x, y] = icon;

                } else {
                    Console.WriteLine("That square was already taken. Try Again.");
                }

            } while (canPlace == false);


            // SETTING THE CHAR ON THE INDEX
            board[x, y] = icon;

            //CHECK FOR OUTCOMES
            int winLoseDraw = WinCheck(board, player);

            return winLoseDraw;
        }


        static (int, int) GetPlayerChoices(int player) {

            string playerChoices = "";

            do {
                Console.WriteLine();
                playerChoices = Input($"Player {player}, please enter x and y coordinates like so, \"1 2\", seperated by a single space: ");

            } while (playerChoices.Length != 3
               || char.IsNumber(playerChoices[0]) == false
               || playerChoices[1] != ' '
               || char.IsNumber(playerChoices[2]) == false
               || playerChoices[0] > 50 || playerChoices[0] < 48
               || playerChoices[2] > 50 || playerChoices[2] < 48
               || string.IsNullOrEmpty(playerChoices)
               || string.IsNullOrWhiteSpace(playerChoices)
            );

            string removed = StringRemove(playerChoices, ' ');

            char[] choices = removed.ToCharArray();

            int x = (int)Char.GetNumericValue(choices[0]);
            int y = (int)Char.GetNumericValue(choices[1]);

            return (x, y);

        }//END GETPLAYERCHOICES

        static string StringRemove(string text, char letter) {
            int count = 0;

            //Count number of occurences of char 'letter' in string "text"
            for (int i = 0; i < text.Length; i++) {
                if (text[i] == letter) {
                    count++;
                }
            }

            //declare new string to hold only chars not equal to 'letter'
            string removedString = "";
            //check each char in "text"; If char not equal to letter, add to string "removedString"
            for (int j = 0; j < text.Length; j++) {
                if (text[j] != letter) {
                    removedString += text[j];
                }
            }
            return removedString;
        }


        static void DrawBoard(string[,] board) {

            int count = 0;
            string line1 = "\tY0\t\tY1\t\tY2";
            string cross = "      ──────────┼───────────────┼──────────";

            Console.WriteLine("               TIC TAC TOE               ");
            Console.WriteLine("──────────────────────────────────────────");
            Console.WriteLine($"Player 1 - X\t\t      Player 2 - O\n");
            //Console.WriteLine($"Player1: {p1wins}\tDraw:{draws}\t\tPlayer2:{p2wins}");

            //Console.WriteLine("\n");

            Console.WriteLine(line1);
            Console.WriteLine();

            // FOR EACH INDEX 
            for (int x = 0; x < board.GetLength(0); x++) {

                //LABEL COORDS
                Console.Write($"X{count}");

                for (int y = 0; y < board.GetLength(1); y++) {

                    //IF FIRST OR MIDDLE COORDS
                    if (y == 0 || y == 1) {

                        //IF ASTERISK
                        if (board[x, y] == "*") {

                            //PRINT SPACE AND VERTICAL LINE
                            Console.Write($"\t \t│");

                        } else {

                            //PRINT CHAR AT INDEX AND VERTICAL LINE
                            Console.Write($"\t{board[x, y]}\t│");
                        }//END IF

                    } else {
                        //IF END OF ROW
                        if (board[x, y] == "*") {

                            //PRINT SPACE
                            Console.Write($"\t \t");
                        } else {

                            //PRINT CHAR AT INDEX
                            Console.Write($"\t{board[x, y]}");
                        }//END IF
                    }//END IF
                }//END FOR

                //TO MAKE SURE CROSS ONLY PRINTS TWICE
                if (count < 2) {

                    Console.WriteLine($"\n{cross}");
                    count++;
                }//END IF

            }//END FOR
            Console.WriteLine("\n");

        }//END DRAWBOARD

        static bool PlaceMarker(string[,] board, string marker, int x_coord, int y_coord) {

            if (board[x_coord, y_coord] == marker) {
                return true;
            } else {
                return false;
            }
        }//END PLACEMARKER

        static int WinCheck(string[,] board, int player) {

            string icon = "";

            if (player == 1) {
                icon = "X";
            } else {
                icon = "O";
            }

            //IF THREE IN A ROW
            if (board[0, 0] == icon && board[1, 0] == icon && board[2, 0] == icon) {
                return 1;
            } else if (board[0, 1] == icon && board[1, 1] == icon && board[2, 1] == icon) {
                return 1;
            } else if (board[0, 2] == icon && board[1, 2] == icon && board[2, 2] == icon) {
                return 1;
            } else if (board[0, 0] == icon && board[0, 1] == icon && board[0, 2] == icon) {
                return 1;
            } else if (board[1, 0] == icon && board[1, 1] == icon && board[1, 2] == icon) {
                return 1;
            } else if (board[2, 0] == icon && board[2, 1] == icon && board[2, 2] == icon) {
                return 1;
            } else if (board[0, 0] == icon && board[1, 1] == icon && board[2, 2] == icon) {
                return 1;
            } else if (board[2, 0] == icon && board[1, 1] == icon && board[0, 2] == icon) {
                return 1;

            } else {

                //FOR EACH INDEX
                for (int i = 0; i < board.GetLength(0); i++) {
                    for (int j = 0; j < board.GetLength(1); j++) {

                        //IF ASTERISK REMAINS
                        if (board[i, j] == "*") {

                            //RETURN CONTINUE
                            return 2;
                        }
                    }
                }
            }

            //ELSE RETURN DRAW
            return 0;
        }

        static string Input(string prompt) {
            Console.Write(prompt);
            return Console.ReadLine();
        }//end input

        static int IntInputVal(string message) {
            int parsedValue = 0;

            while (int.TryParse(Input(message), out parsedValue) == false) {
                Console.WriteLine("\nInvalid Entry");
            }
            return parsedValue;
        }//end int input validation

    }//end class
}//namespace


