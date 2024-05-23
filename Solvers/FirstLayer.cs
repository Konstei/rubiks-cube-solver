using static Cube;

public static class FirstLayer {
    public static void Solve(Cube cube) { 
        while (!(
            cube.Corners[0].SequenceEqual(new List<Color>() { Color.WHITE, Color.ORANGE, Color.GREEN }) &&
            cube.Corners[1].SequenceEqual(new List<Color>() { Color.WHITE, Color.GREEN, Color.RED }) &&
            cube.Corners[2].SequenceEqual(new List<Color>() { Color.WHITE, Color.RED, Color.BLUE }) &&
            cube.Corners[3].SequenceEqual(new List<Color>() { Color.WHITE, Color.BLUE, Color.ORANGE })
        )) {
            int corner = FindMatching(cube);
            if (corner == -1) {
                cube.RotateFace(Color.YELLOW, conf: true);
                continue;
            }
            switch (corner) {
                case 4: case 8:  FourMoves(cube, Color.ORANGE); cube.RotateFace(Color.YELLOW, conf: true); break;
                case 5: case 10: FourMoves(cube, Color.GREEN); cube.RotateFace(Color.YELLOW, conf: true); break;
                case 6: case 12: FourMoves(cube, Color.RED); cube.RotateFace(Color.YELLOW, conf: true); break;
                case 7: case 14: FourMoves(cube, Color.BLUE); cube.RotateFace(Color.YELLOW, conf: true); break;
            }
            if (corner > 7) continue;
            if (corner > 3) corner %= 4;
            switch (corner) {
            case 0:
                while (!cube.Corners[0].SequenceEqual(new List<Color>() { Color.WHITE, Color.ORANGE, Color.GREEN })) {
                    FourMoves(cube, Color.ORANGE); FourMoves(cube, Color.ORANGE);
                    cube.Configure();
                }
                break;

            case 1:
                while (!cube.Corners[1].SequenceEqual(new List<Color>() { Color.WHITE, Color.GREEN, Color.RED })) {
                    FourMoves(cube, Color.GREEN); FourMoves(cube, Color.GREEN);
                    cube.Configure();
                }
                break;

            case 2:
                while (!cube.Corners[2].SequenceEqual(new List<Color>() { Color.WHITE, Color.RED, Color.BLUE })) {
                    FourMoves(cube, Color.RED); FourMoves(cube, Color.RED);
                    cube.Configure();
                }
                break;

            case 3:
                while (!cube.Corners[3].SequenceEqual(new List<Color>() { Color.WHITE, Color.BLUE, Color.ORANGE })) {
                    FourMoves(cube, Color.BLUE); FourMoves(cube, Color.BLUE);
                    cube.Configure();
                }
                break;

            default:
                cube.RotateFace(Color.YELLOW);
                break;
            }
            cube.Configure();
            cube.RotateFace(Color.YELLOW);
        }
    }

    private static void FourMoves(Cube cube, Color color) {
        cube.RotateFace(color);
        cube.RotateFace(Color.YELLOW);
        cube.RotateFace(color); cube.RotateFace(color); cube.RotateFace(color);
        cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW);
    }

    private static int FindMatching(Cube cube) {
        if (cube.Corners[0].Contains(Color.WHITE) && cube.Corners[0].Contains(Color.ORANGE) && cube.Corners[0].Contains(Color.GREEN)  && !cube.Corners[0].SequenceEqual(new List<Color>() { Color.WHITE, Color.ORANGE, Color.GREEN })) return 0;
        if (cube.Corners[1].Contains(Color.WHITE) && cube.Corners[1].Contains(Color.GREEN)  && cube.Corners[1].Contains(Color.RED)    && !cube.Corners[1].SequenceEqual(new List<Color>() { Color.WHITE, Color.GREEN, Color.RED })) return 1;
        if (cube.Corners[2].Contains(Color.WHITE) && cube.Corners[2].Contains(Color.RED)    && cube.Corners[2].Contains(Color.BLUE)   && !cube.Corners[2].SequenceEqual(new List<Color>() { Color.WHITE, Color.RED, Color.BLUE })) return 2;
        if (cube.Corners[3].Contains(Color.WHITE) && cube.Corners[3].Contains(Color.BLUE)   && cube.Corners[3].Contains(Color.ORANGE) && !cube.Corners[3].SequenceEqual(new List<Color>() { Color.WHITE, Color.BLUE, Color.ORANGE })) return 3;
        if (cube.Corners[4].Contains(Color.WHITE) && cube.Corners[4].Contains(Color.ORANGE) && cube.Corners[4].Contains(Color.GREEN)) return 4;
        if (cube.Corners[5].Contains(Color.WHITE) && cube.Corners[5].Contains(Color.GREEN) && cube.Corners[5].Contains(Color.RED)) return 5;
        if (cube.Corners[6].Contains(Color.WHITE) && cube.Corners[6].Contains(Color.RED) && cube.Corners[6].Contains(Color.BLUE)) return 6;
        if (cube.Corners[7].Contains(Color.WHITE) && cube.Corners[7].Contains(Color.BLUE) && cube.Corners[7].Contains(Color.ORANGE)) return 7;
        if (cube.Corners[0].Contains(Color.WHITE) && cube.Corners[1].Contains(Color.WHITE) && cube.Corners[2].Contains(Color.WHITE) && cube.Corners[3].Contains(Color.WHITE)) {
            if (!cube.Corners[0].SequenceEqual(new List<Color>() { Color.WHITE, Color.ORANGE, Color.GREEN })) return 8;
            if (!cube.Corners[1].SequenceEqual(new List<Color>() { Color.WHITE, Color.GREEN, Color.RED })) return 10;
            if (!cube.Corners[2].SequenceEqual(new List<Color>() { Color.WHITE, Color.RED, Color.BLUE })) return 12;
            if (!cube.Corners[3].SequenceEqual(new List<Color>() { Color.WHITE, Color.BLUE, Color.ORANGE })) return 14;
        }
        return -1;
    }
}