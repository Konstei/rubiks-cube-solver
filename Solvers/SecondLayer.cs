using System.Globalization;
using static Cube;

public static class SecondLayer {
    public static void Solve(Cube cube) {
        while (!(
            cube.Edges[1][2] == Color.ORANGE && cube.Edges[2][1] == Color.GREEN &&
            cube.Edges[2][3] == Color.GREEN && cube.Edges[3][2] == Color.RED &&
            cube.Edges[3][4] == Color.RED && cube.Edges[4][3] == Color.BLUE &&
            cube.Edges[4][1] == Color.BLUE && cube.Edges[1][4] == Color.ORANGE
        )) {
            // first top then side
            // - = left/right
            // + = right/left
            int edge = FindMatching(cube);
            int sign = edge < 0 ? -1 : edge > 0 ? 1 : 0;
            edge *= sign;

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
            // do this
        }
        return 0;
    }

    private static void FourMovesLeft(Cube cube, Color face) {
        // rotate face 3 times
        // rotate yellow 3 times
        // rotate face
        // rotate yellow
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW);
        cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW);
    }

    private static void FourMovesRight(Cube cube, Color face) {
        // rotate face on side
        // rotate yellow
        // rotate face on side 3 times
        // rotate yellow 3 times
        cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW);
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW); cube.RotateFace(Color.YELLOW);
    }
}