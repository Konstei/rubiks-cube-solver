using static Cube;

public static class SL {
    public static void Solve(Cube cube) {
        while (!(
            cube.Edges[1][2] == Color.ORANGE && cube.Edges[2][1] == Color.GREEN &&
            cube.Edges[2][3] == Color.GREEN && cube.Edges[3][2] == Color.RED &&
            cube.Edges[3][4] == Color.RED && cube.Edges[4][3] == Color.BLUE &&
            cube.Edges[4][1] == Color.BLUE && cube.Edges[1][4] == Color.ORANGE
        )) {
            int edge = FindMatching(cube);
            int sign = edge < 0 ? -1 : edge > 0 ? 1 : 0;
            edge *= sign;

            switch (edge) {
            case 12:
                FourMovesRight(cube, Color.ORANGE);
                FourMovesLeft(cube, Color.GREEN);
                break;
            case 23:
                FourMovesRight(cube, Color.GREEN);
                FourMovesLeft(cube, Color.RED);
                break;
            case 34:
                FourMovesRight(cube, Color.RED);
                FourMovesLeft(cube, Color.BLUE);
                break;
            case 41:
                FourMovesRight(cube, Color.BLUE);
                FourMovesLeft(cube, Color.ORANGE);
                break;
            }
            if (edge > 10) {
                cube.Configure();
                continue;
            }

            switch (sign) {
            case 0:
                cube.RotateFace(Color.YELLOW);
                break;
            case -1:
                FourMovesLeft(cube, cube.Edges[5][edge]);
                FourMovesRight(cube, cube.Edges[edge][5]);
                break;

            case 1:
                FourMovesRight(cube, cube.Edges[5][edge]);
                FourMovesLeft(cube, cube.Edges[edge][5]);
                break;
            }
            cube.Configure();
        }
    }

    private static int FindMatching(Cube cube) {
        if (cube.Edges[4][5] == Color.ORANGE && cube.Edges[5][4] == Color.GREEN) return -4;
        if (cube.Edges[3][5] == Color.GREEN && cube.Edges[5][3] == Color.ORANGE) return 3;
        if (cube.Edges[1][5] == Color.GREEN && cube.Edges[5][1] == Color.RED) return -1;
        if (cube.Edges[4][5] == Color.RED && cube.Edges[5][4] == Color.GREEN) return 4;
        if (cube.Edges[2][5] == Color.RED && cube.Edges[5][2] == Color.BLUE) return -2;
        if (cube.Edges[1][5] == Color.BLUE && cube.Edges[5][1] == Color.RED) return 1;
        if (cube.Edges[3][5] == Color.BLUE && cube.Edges[5][3] == Color.ORANGE) return -3;
        if (cube.Edges[2][5] == Color.ORANGE && cube.Edges[5][2] == Color.BLUE) return 2;
        if (
            (cube.Edges[1][5] == Color.YELLOW || cube.Edges[5][1] == Color.YELLOW) &&
            (cube.Edges[2][5] == Color.YELLOW || cube.Edges[5][2] == Color.YELLOW) &&
            (cube.Edges[3][5] == Color.YELLOW || cube.Edges[5][3] == Color.YELLOW) &&
            (cube.Edges[4][5] == Color.YELLOW || cube.Edges[5][4] == Color.YELLOW)
        ) {
            if (!(cube.Edges[1][2] == Color.ORANGE && cube.Edges[2][1] == Color.GREEN)) return 12;
            if (!(cube.Edges[2][3] == Color.GREEN && cube.Edges[3][2] == Color.RED)) return 23;
            if (!(cube.Edges[3][4] == Color.RED && cube.Edges[4][3] == Color.BLUE)) return 34;
            if (!(cube.Edges[4][1] == Color.BLUE && cube.Edges[1][4] == Color.ORANGE)) return 41;
        }
        return 0;
    }

    private static void FourMovesLeft(Cube cube, Color face) {
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW);
        cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW);
    }

    private static void FourMovesRight(Cube cube, Color face) {
        cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW);
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW);
    }
}