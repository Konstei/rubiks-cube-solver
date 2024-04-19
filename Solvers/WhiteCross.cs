using static Cube;

public static class WhiteCross {
    public static void Solve(Cube cube) {
        while (!(
            cube.Faces[5][1] == Color.WHITE &&
            cube.Faces[5][3] == Color.WHITE &&
            cube.Faces[5][4] == Color.WHITE &&
            cube.Faces[5][6] == Color.WHITE
        )) {
            
        }
    }
}
