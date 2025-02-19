public class Program {
    public static void Main() {
        Cube scrambled = new();
        scrambled.Print();
        Console.WriteLine();

        List<Move> mm = new();// = [Move.Lr, Move.U2, Move.R2, Move.B2, Move.L, Move.Br, Move.Lr, Move.R, Move.F2, Move.U, Move.R, Move.B, Move.U, Move.D, Move.R2, Move.F, Move.D2, Move.Lr, Move.Fr, Move.U, Move.F, Move.Dr, Move.L, Move.D2, Move.R2];
        Random random = new();
        int r = random.Next(0, 100);
        for (int i=0; i<r; i++) mm.Add((Move)random.Next(1, 19));
        // Console.WriteLine("populated");
        Scramble(scrambled, mm);
        // Console.WriteLine("scrambled");
        scrambled.Print();
        Console.WriteLine();

        Cube cube = scrambled;

        // cube.Validate();

        WhiteCross.Solve(cube);
        FirstLayer.Solve(cube);
        SecondLayer.Solve(cube);
        LastLayer.Solve(cube);

        cube.Print();
        Console.WriteLine();
        foreach (Move m in ColorsToMoves(cube)) Console.Write($"{m} ");
        Console.WriteLine();
        Console.ReadKey();
    }

    private static void Scramble(Cube cube, List<Move> mm) {
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

    private static void ReduceMoveList(Cube initial, Cube final) {
        Cube cl = initial;
        Cube cr = cl;
        List<Cube.Color> reducedMoves = final.Moves;

        int ld = -1;

        for (int ir=0; ir < reducedMoves.Count; ir++) {
            cr.RotateFace(reducedMoves[ir]);
            if (cr.Faces.SequenceEqual(cl.Faces)) ld = ir;
        }
        {
            List<Cube.Color> erased = new(ld+1);
            erased.AddRange(Enumerable.Repeat(Cube.Color.NONE, ld+1));
            reducedMoves.RemoveRange(0, ld+1);
            reducedMoves.InsertRange(0, erased);
        }

        for (int il=0; il < reducedMoves.Count-1; il++) {
            ld = -1;
            cl.RotateFace(reducedMoves[il]);
            cr = cl;
            for (int ir=il+1; ir < reducedMoves.Count; ir++) {
                cr.RotateFace(reducedMoves[ir]);
                if (cr.Faces.SequenceEqual(cl.Faces)) ld = ir;
            }
            List<Cube.Color> erased = new(ld+1);
            erased.AddRange(Enumerable.Repeat(Cube.Color.NONE, ld+1));
            reducedMoves.RemoveRange(il+1, ld+1);
            reducedMoves.InsertRange(il+1, erased);
        }
    }

    enum Move {
        NONE,
        U, L, D, R, F, B,
        U2, L2, D2, R2, F2, B2,
        Ur, Lr, Dr, Rr, Fr, Br
    }
}
