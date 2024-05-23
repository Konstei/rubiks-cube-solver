public class Program {
    public static void Main() {
        Cube cube = new(
            new List<Cube.Color> {
                Cube.Color.WHITE, Cube.Color.WHITE, Cube.Color.WHITE,
                Cube.Color.WHITE,                   Cube.Color.WHITE,
                Cube.Color.WHITE, Cube.Color.WHITE, Cube.Color.WHITE
            },
            new List<Cube.Color> {
                Cube.Color.ORANGE, Cube.Color.ORANGE, Cube.Color.ORANGE,
                Cube.Color.ORANGE,                    Cube.Color.ORANGE,
                Cube.Color.ORANGE, Cube.Color.ORANGE, Cube.Color.ORANGE
            },
            new List<Cube.Color> {
                Cube.Color.GREEN, Cube.Color.GREEN, Cube.Color.GREEN,
                Cube.Color.GREEN,                   Cube.Color.GREEN,
                Cube.Color.GREEN, Cube.Color.GREEN, Cube.Color.GREEN
            },
            new List<Cube.Color> {
                Cube.Color.RED, Cube.Color.RED, Cube.Color.RED,
                Cube.Color.RED,                 Cube.Color.RED,
                Cube.Color.RED, Cube.Color.RED, Cube.Color.RED
            },
            new List<Cube.Color> {
                Cube.Color.BLUE, Cube.Color.BLUE, Cube.Color.BLUE,
                Cube.Color.BLUE,                  Cube.Color.BLUE,
                Cube.Color.BLUE, Cube.Color.BLUE, Cube.Color.BLUE
            },
            new List<Cube.Color> {
                Cube.Color.YELLOW, Cube.Color.YELLOW, Cube.Color.YELLOW,
                Cube.Color.YELLOW,                    Cube.Color.YELLOW,
                Cube.Color.YELLOW, Cube.Color.YELLOW, Cube.Color.YELLOW
            }
        );

        Move[] mm = {Move.Lr, Move.U2, Move.R2, Move.B2, Move.L, Move.Br, Move.Lr, Move.R, Move.F2, Move.U, Move.R, Move.B, Move.U, Move.D, Move.R2, Move.F, Move.D2, Move.Lr, Move.Fr, Move.U, Move.F, Move.Dr, Move.L, Move.D2, Move.R2};
        Scramble(cube, mm);
        cube.Print();
        Console.WriteLine();

        WhiteCross.Solve(cube);
        cube.Print();
        Console.WriteLine();

        FirstLayer.Solve(cube);
        cube.Print();
        Console.WriteLine();

        cube.Moves.Clear();
    }

    private static void Scramble(Cube cube, Move[] mm) {
        foreach (Move m in mm) {
            switch (m) {
                case Move.U:  cube.RotateFace(Cube.Color.WHITE);  break;
                case Move.L:  cube.RotateFace(Cube.Color.ORANGE); break;
                case Move.D:  cube.RotateFace(Cube.Color.YELLOW); break;
                case Move.R:  cube.RotateFace(Cube.Color.RED);    break;
                case Move.F:  cube.RotateFace(Cube.Color.GREEN);  break;
                case Move.B:  cube.RotateFace(Cube.Color.BLUE);   break;

                case Move.U2: cube.RotateFace(Cube.Color.WHITE);  cube.RotateFace(Cube.Color.WHITE);  break;
                case Move.L2: cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE); break;
                case Move.D2: cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW); break;
                case Move.R2: cube.RotateFace(Cube.Color.RED);    cube.RotateFace(Cube.Color.RED);    break;
                case Move.F2: cube.RotateFace(Cube.Color.GREEN);  cube.RotateFace(Cube.Color.GREEN);  break;
                case Move.B2: cube.RotateFace(Cube.Color.BLUE);   cube.RotateFace(Cube.Color.BLUE);   break;

                case Move.Ur: cube.RotateFace(Cube.Color.WHITE);  cube.RotateFace(Cube.Color.WHITE);  cube.RotateFace(Cube.Color.WHITE);  break;
                case Move.Lr: cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE); break;
                case Move.Dr: cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW); break;
                case Move.Rr: cube.RotateFace(Cube.Color.RED);    cube.RotateFace(Cube.Color.RED);    cube.RotateFace(Cube.Color.RED);    break;
                case Move.Fr: cube.RotateFace(Cube.Color.GREEN);  cube.RotateFace(Cube.Color.GREEN);  cube.RotateFace(Cube.Color.GREEN);  break;
                case Move.Br: cube.RotateFace(Cube.Color.BLUE);   cube.RotateFace(Cube.Color.BLUE);   cube.RotateFace(Cube.Color.BLUE);   break;
            }
        }
        cube.Configure();
    }

    private static List<Move> ColorsToMoves(Cube cube) {
        List<Move> moves = new();
        foreach (Cube.Color move in cube.Moves) {
            switch (move) {
                case Cube.Color.WHITE:  moves.Add(Move.U); break;
                case Cube.Color.ORANGE: moves.Add(Move.L); break;
                case Cube.Color.GREEN:  moves.Add(Move.F); break;
                case Cube.Color.RED:    moves.Add(Move.R); break;
                case Cube.Color.BLUE:   moves.Add(Move.B); break;
                case Cube.Color.YELLOW: moves.Add(Move.D); break;
            }
        }
        return moves;
    }

    enum Move {
        U, L, D, R, F, B,
        U2, L2, D2, R2, F2, B2,
        Ur, Lr, Dr, Rr, Fr, Br
    }
}
