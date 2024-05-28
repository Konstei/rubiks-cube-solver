using static Cube;

public static class WC {
    public static void Solve(Cube cube) {
        for (int e=0; e<4; e++) {
            List<Color> face = cube.Edges.FirstOrDefault(innerList => innerList.Contains(Color.WHITE)) ?? throw new UniverseException("the name of the exception should be self explanatory really");

            switch (cube.Edges.IndexOf(face)) {
            case 0:
                // WHITE
                switch (face.IndexOf(Color.WHITE)) {
                case 4:
                    while (cube.Faces[5][6] == Color.WHITE) cube.RotateFace(Color.YELLOW);
                    cube.RotateFace(Color.BLUE); cube.RotateFace(Color.BLUE);
                    break;
                case 1:
                    while (cube.Faces[5][3] == Color.WHITE) cube.RotateFace(Color.YELLOW);
                    cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE);
                    break;
                case 3:
                    while (cube.Faces[5][4] == Color.WHITE) cube.RotateFace(Color.YELLOW);
                    cube.RotateFace(Color.RED); cube.RotateFace(Color.RED);
                    break;
                case 2:
                    while (cube.Faces[5][1] == Color.WHITE) cube.RotateFace(Color.YELLOW);
                    cube.RotateFace(Color.GREEN); cube.RotateFace(Color.GREEN);
                    break;
                }
                break;
            
            case 1:
                // ORANGE
                switch (face.IndexOf(Color.WHITE)) {
                case 5:
                    cube.RotateFace(Color.ORANGE);
                    while (cube.Edges[5][4] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.BLUE);
                    break;
                case 4:
                    while (cube.Edges[5][4] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.BLUE);
                    break;
                case 0:
                    while (cube.Edges[5][1] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.ORANGE);
                    while (cube.Edges[5][2] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.GREEN); cube.RotateFace(Color.GREEN); cube.RotateFace(Color.GREEN);
                    break;
                case 2:
                    while (cube.Edges[5][2] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.GREEN); cube.RotateFace(Color.GREEN); cube.RotateFace(Color.GREEN);
                    break;
                }
                break;

            case 2:
                // GREEN
                switch (face.IndexOf(Color.WHITE)) {
                case 5:
                    cube.RotateFace(Color.GREEN);
                    while (cube.Edges[5][1] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.ORANGE);
                    break;
                case 1:
                    while (cube.Edges[5][1] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.ORANGE);
                    break;
                case 0:
                    while (cube.Edges[5][2] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.GREEN);
                    while (cube.Edges[5][3] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.RED); cube.RotateFace(Color.RED); cube.RotateFace(Color.RED);
                    break;
                case 3:
                    while (cube.Edges[5][3] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.RED); cube.RotateFace(Color.RED); cube.RotateFace(Color.RED);
                    break;
                }
                break;

            case 3:
                // RED
                switch (face.IndexOf(Color.WHITE)) {
                case 5:
                    cube.RotateFace(Color.RED);
                    while (cube.Edges[5][2] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.GREEN);
                    break;
                case 2:
                    while (cube.Edges[5][2] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.GREEN);
                    break;
                case 0:
                    while (cube.Edges[5][3] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.RED);
                    while (cube.Edges[5][4] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.BLUE); cube.RotateFace(Color.BLUE); cube.RotateFace(Color.BLUE);
                    break;
                case 4:
                    while (cube.Edges[5][4] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.BLUE); cube.RotateFace(Color.BLUE); cube.RotateFace(Color.BLUE);
                    break;
                }
                break;

            case 4:
                // BLUE
                switch (face.IndexOf(Color.WHITE)) {
                case 5:
                    cube.RotateFace(Color.BLUE);
                    while (cube.Edges[5][3] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.RED);
                    break;
                case 3:
                    while (cube.Edges[5][3] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.RED);
                    break;
                case 0:
                    while (cube.Edges[5][4] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.BLUE);
                    while (cube.Edges[5][1] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE);
                    break;
                case 1:
                    while (cube.Edges[5][1] == Color.WHITE) cube.RotateFace(Color.YELLOW, conf: true);
                    cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE);
                    break;
                }
                break;
            }
            
            cube.Configure();
        }
        
        while (
            new List<Color>() {
                cube.Edges[5][1],
                cube.Edges[5][2],
                cube.Edges[5][3],
                cube.Edges[5][4]
            }.Contains(Color.WHITE)
        ) {
            switch (FindMatching(cube)) {
                case Color.ORANGE: cube.RotateFace(Color.ORANGE); cube.RotateFace(Color.ORANGE); break;
                case Color.GREEN:  cube.RotateFace(Color.GREEN);  cube.RotateFace(Color.GREEN);  break;
                case Color.RED:    cube.RotateFace(Color.RED);    cube.RotateFace(Color.RED);    break;
                case Color.BLUE:   cube.RotateFace(Color.BLUE);   cube.RotateFace(Color.BLUE);   break;
                default: cube.RotateFace(Color.YELLOW); break;
            }
            cube.Configure();
        }
    }

    private static Color FindMatching(Cube cube) {
        for (int f=1; f<=4; f++) {
            if (((int) cube.Edges[f][5]) == f && cube.Edges[5][f] == Color.WHITE) return (Color) (f);
        }
        return Color.NONE;
    }
}

[Serializable]
class UniverseException : Exception {
    public UniverseException() {}

    public UniverseException(string message)
    : base(message)
    {}
}