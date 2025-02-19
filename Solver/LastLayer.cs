using static Cube;

public static class LastLayer {
    public static void Solve(Cube cube) {
        YellowCross(cube);
        MatchEdges(cube);
        // MatchCorners(cube);
        // OrientCorners(cube);
    }

    private static void YellowCross(Cube cube) {
        while (!(
            cube.Edges[5][1] == Color.YELLOW &&
            cube.Edges[5][2] == Color.YELLOW &&
            cube.Edges[5][3] == Color.YELLOW &&
            cube.Edges[5][4] == Color.YELLOW
        )) {
            if (cube.Edges[5][1] == Color.YELLOW && cube.Edges[5][3] == Color.YELLOW) {
                cube.RotateFace(Color.GREEN);
                FourMovesBottom(cube, Color.ORANGE);
                cube.RotateFace(Color.GREEN); cube.RotateFace(Color.GREEN); cube.RotateFace(Color.GREEN, conf: true);
                continue;
            }
            if (cube.Edges[5][2] == Color.YELLOW && cube.Edges[5][4] == Color.YELLOW) {
                cube.RotateFace(Color.ORANGE);
                FourMovesBottom(cube, Color.BLUE);
                cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE, conf: true);
                continue;
            }

            if (cube.Edges[5][1] == Color.YELLOW && cube.Edges[5][2] == Color.YELLOW) {
                cube.RotateFace(Color.BLUE);
                FourMovesBottom(cube, Color.RED);
                cube.RotateFace(Color.BLUE); cube.RotateFace(Color.BLUE); cube.RotateFace(Color.BLUE, conf: true);
                continue;
            }
            if (cube.Edges[5][2] == Color.YELLOW && cube.Edges[5][3] == Color.YELLOW) {
                cube.RotateFace(Color.ORANGE);
                FourMovesBottom(cube, Color.BLUE);
                cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE, conf: true);
                continue;
            }
            if (cube.Edges[5][3] == Color.YELLOW && cube.Edges[5][4] == Color.YELLOW) {
                cube.RotateFace(Color.GREEN);
                FourMovesBottom(cube, Color.ORANGE);
                cube.RotateFace(Color.GREEN); cube.RotateFace(Color.GREEN); cube.RotateFace(Color.GREEN, conf: true);
                continue;
            }
            if (cube.Edges[5][4] == Color.YELLOW && cube.Edges[5][1] == Color.YELLOW) {
                cube.RotateFace(Color.RED);
                FourMovesBottom(cube, Color.GREEN);
                cube.RotateFace(Color.RED); cube.RotateFace(Color.RED); cube.RotateFace(Color.RED, conf: true);
                continue;
            }

            if (cube.Edges[1][5] == Color.YELLOW && cube.Edges[2][5] == Color.YELLOW) {
                cube.RotateFace(Color.GREEN);
                FourMovesBottom(cube, Color.ORANGE);
                cube.RotateFace(Color.GREEN); cube.RotateFace(Color.GREEN); cube.RotateFace(Color.GREEN, conf: true);
                continue;
            }
            if (cube.Edges[2][5] == Color.YELLOW && cube.Edges[3][5] == Color.YELLOW) {
                cube.RotateFace(Color.RED);
                FourMovesBottom(cube, Color.GREEN);
                cube.RotateFace(Color.RED); cube.RotateFace(Color.RED); cube.RotateFace(Color.RED, conf: true);
                continue;
            }
            if (cube.Edges[3][5] == Color.YELLOW && cube.Edges[4][5] == Color.YELLOW) {
                cube.RotateFace(Color.BLUE);
                FourMovesBottom(cube, Color.RED);
                cube.RotateFace(Color.BLUE); cube.RotateFace(Color.BLUE); cube.RotateFace(Color.BLUE, conf: true);
                continue;
            }
            if (cube.Edges[4][5] == Color.YELLOW && cube.Edges[1][5] == Color.YELLOW) {
                cube.RotateFace(Color.ORANGE);
                FourMovesBottom(cube, Color.BLUE);
                cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE, conf: true);
                continue;
            }
        }
    }

    private static void MatchEdges(Cube cube) {
        while (
            Convert.ToInt32(cube.Edges[1][5] == Color.ORANGE) +
            Convert.ToInt32(cube.Edges[2][5] == Color.GREEN) +
            Convert.ToInt32(cube.Edges[3][5] == Color.RED) +
            Convert.ToInt32(cube.Edges[4][5] == Color.BLUE) == 2 ||
            Convert.ToInt32(cube.Edges[1][5] == Color.ORANGE) +
            Convert.ToInt32(cube.Edges[2][5] == Color.GREEN) +
            Convert.ToInt32(cube.Edges[3][5] == Color.RED) +
            Convert.ToInt32(cube.Edges[4][5] == Color.BLUE) == 4
        ) cube.RotateFace(Color.YELLOW, true);

        if (
            Convert.ToInt32(cube.Edges[1][5] == Color.ORANGE) +
            Convert.ToInt32(cube.Edges[2][5] == Color.GREEN) +
            Convert.ToInt32(cube.Edges[3][5] == Color.RED) +
            Convert.ToInt32(cube.Edges[4][5] == Color.BLUE) == 4
        ) return;

        if (cube.Edges[1][5] == Color.ORANGE && cube.Edges[3][5] == Color.RED) {
            cube.RotateFace(Color.YELLOW);
            FourMovesVariation(cube, Color.BLUE);
            FourMovesVariation(cube, Color.GREEN);
        }
        else if (cube.Edges[2][5] == Color.GREEN && cube.Edges[4][5] == Color.BLUE) {
            cube.RotateFace(Color.YELLOW);
            FourMovesVariation(cube, Color.ORANGE);
            FourMovesVariation(cube, Color.RED);
        }
        else if (cube.Edges[1][5] == Color.ORANGE && cube.Edges[2][5] == Color.GREEN) FourMovesVariation(cube, Color.GREEN);
        else if (cube.Edges[2][5] == Color.GREEN && cube.Edges[3][5] == Color.RED) FourMovesVariation(cube, Color.RED);
        else if (cube.Edges[3][5] == Color.RED && cube.Edges[4][5] == Color.BLUE) FourMovesVariation(cube, Color.BLUE);
        else if (cube.Edges[4][5] == Color.BLUE && cube.Edges[1][5] == Color.ORANGE) FourMovesVariation(cube, Color.ORANGE);

        // if (
        //     Convert.ToInt32(cube.Edges[1][5] == Color.ORANGE) +
        //     Convert.ToInt32(cube.Edges[2][5] == Color.GREEN) +
        //     Convert.ToInt32(cube.Edges[3][5] == Color.RED) +
        //     Convert.ToInt32(cube.Edges[4][5] == Color.BLUE) == 4
        // ) return;
    }

    private static void MatchCorners(Cube cube) {
        while (!(
            cube.Corners[4].Contains(Color.YELLOW) && cube.Corners[4].Contains(Color.ORANGE) && cube.Corners[4].Contains(Color.GREEN) &&
            cube.Corners[5].Contains(Color.YELLOW) && cube.Corners[5].Contains(Color.GREEN) && cube.Corners[5].Contains(Color.RED) &&
            cube.Corners[6].Contains(Color.YELLOW) && cube.Corners[6].Contains(Color.RED) && cube.Corners[6].Contains(Color.BLUE) &&
            cube.Corners[7].Contains(Color.YELLOW) && cube.Corners[7].Contains(Color.BLUE) && cube.Corners[7].Contains(Color.ORANGE)
        )) {
            int corner = FindMatching(cube);
            switch (corner) {
                case -1:
                case 1: PermutationMoves(cube, Color.RED, Color.ORANGE); break;
                case 2: PermutationMoves(cube, Color.BLUE, Color.GREEN); break;
                case 3: PermutationMoves(cube, Color.ORANGE, Color.RED); break;
                case 4: PermutationMoves(cube, Color.GREEN, Color.BLUE); break;
            }
            cube.Configure();
        }
    }

    private static void OrientCorners(Cube cube) {
        for (int f=0; f<4; f++) {
            while (cube.Corners[7][0] != Color.YELLOW) {
                FourMovesTop(cube, Color.ORANGE);
                FourMovesTop(cube, Color.ORANGE);
                cube.Configure();
            }
            cube.RotateFace(Color.YELLOW, conf: true);
        }
    }

    private static int FindMatching(Cube cube) {
        if (cube.Corners[4].Contains(Color.YELLOW) && cube.Corners[4].Contains(Color.ORANGE) && cube.Corners[4].Contains(Color.GREEN)) return 1;
        if (cube.Corners[5].Contains(Color.YELLOW) && cube.Corners[5].Contains(Color.GREEN) && cube.Corners[5].Contains(Color.RED)) return 2;
        if (cube.Corners[6].Contains(Color.YELLOW) && cube.Corners[6].Contains(Color.RED) && cube.Corners[6].Contains(Color.BLUE)) return 3;
        if (cube.Corners[7].Contains(Color.YELLOW) && cube.Corners[7].Contains(Color.BLUE) && cube.Corners[7].Contains(Color.ORANGE)) return 4;
        return -1;
    }

    private static void FourMovesBottom(Cube cube, Color face) {
        cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW);
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW);
    }

    private static void FourMovesTop(Cube cube, Color face) {
        cube.RotateFace(face);
        cube.RotateFace(Color.WHITE);
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Color.WHITE); cube.RotateFace(Color.WHITE); cube.RotateFace(Color.WHITE);
    }

    private static void FourMovesVariation(Cube cube, Color face) {
        cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW);
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW);
        cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW);
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW, true);
    }

    private static void PermutationMoves(Cube cube, Color left, Color right) {
        cube.RotateFace(Color.YELLOW);
        cube.RotateFace(right);
        cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW);
        cube.RotateFace(left); cube.RotateFace(left); cube.RotateFace(left);
        cube.RotateFace(Color.YELLOW);
        cube.RotateFace(right); cube.RotateFace(right); cube.RotateFace(right);
        cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW);
        cube.RotateFace(left);
    }
}