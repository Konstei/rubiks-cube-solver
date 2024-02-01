public class Cube {
    public Cube(
        List<Color> whiteFace,
        List<Color> orangeFace,
        List<Color> greenFace,
        List<Color> redFace,
        List<Color> blueFace,
        List<Color> yellowFace
    ) {
        Faces = new() {
            whiteFace, orangeFace, greenFace, redFace, blueFace, yellowFace
        };
        Configure();
        Validate();
    }

    private void Configure() {
        Edges = new() {
            new() { Color.NONE,  Faces[0][3], Faces[0][6], Faces[0][4], Faces[0][1], Color.NONE  },
            new() { Faces[1][1], Color.NONE,  Faces[1][4], Color.NONE,  Faces[1][3], Faces[1][6] },
            new() { Faces[2][1], Faces[2][3], Color.NONE,  Faces[2][4], Color.NONE,  Faces[2][6] },
            new() { Faces[3][1], Color.NONE,  Faces[3][3], Color.NONE,  Faces[3][4], Faces[3][6] },
            new() { Faces[4][1], Faces[4][4], Color.NONE,  Faces[4][3], Color.NONE,  Faces[4][6] },
            new() { Color.NONE,  Faces[5][3], Faces[5][1], Faces[5][4], Faces[5][6], Color.NONE  }
        };
        Corners = new() {
            new() {
                new() { Faces[0][5], Faces[1][2], Faces[2][0] },
                new() { Faces[0][7], Faces[2][2], Faces[3][0] },
                new() { Faces[0][2], Faces[3][2], Faces[4][0] },
                new() { Faces[0][0], Faces[4][2], Faces[1][0] }
            },
            new() {
                new() { Faces[5][0], Faces[1][7], Faces[2][5] },
                new() { Faces[5][2], Faces[2][7], Faces[3][5] },
                new() { Faces[5][7], Faces[3][7], Faces[4][5] },
                new() { Faces[5][5], Faces[4][7], Faces[1][5] }
            },
        };
    }

    public void Validate() {
        // check that each color appears exactly 9 times
        int whiteCount = 0, orangeCount = 0, greenCount = 0, redCount = 0, blueCount = 0, yellowCount = 0;
        foreach (List<Color> face in Faces) {
            foreach (Color color in face) {
                switch (color) {
                case Color.WHITE:
                    whiteCount++;
                    break;
                case Color.ORANGE:
                    orangeCount++;
                    break;
                case Color.GREEN:
                    greenCount++;
                    break;
                case Color.RED:
                    redCount++;
                    break;
                case Color.BLUE:
                    blueCount++;
                    break;
                case Color.YELLOW:
                    yellowCount++;
                    break;
                }
            }
        }
        if (whiteCount != 8 || orangeCount != 8 || greenCount != 8 || redCount != 8 || blueCount != 8 || yellowCount != 8) {
            throw new InvalidCubeConfigurationException("Each color must appear exactly 9 times, including the center");
        }

        // check color adjacency on all edges
        Dictionary<(Color, Color), int> adjacentEdges = new() {
            { ( Color.WHITE,  Color.ORANGE ), 0 },
            { ( Color.WHITE,  Color.GREEN  ), 0 },
            { ( Color.WHITE,  Color.RED    ), 0 },
            { ( Color.WHITE,  Color.BLUE   ), 0 },
            { ( Color.ORANGE, Color.GREEN  ), 0 },
            { ( Color.ORANGE, Color.BLUE   ), 0 },
            { ( Color.GREEN,  Color.RED    ), 0 },
            { ( Color.RED,    Color.BLUE   ), 0 },
            { ( Color.ORANGE, Color.YELLOW ), 0 },
            { ( Color.GREEN,  Color.YELLOW ), 0 },
            { ( Color.RED,    Color.YELLOW ), 0 },
            { ( Color.BLUE,   Color.YELLOW ), 0 },
        };
        List<(int, int)> edges = new() {
            (0, 1),
            (0, 2),
            (0, 3),
            (0, 4),
            (1, 2),
            (1, 4),
            (2, 3),
            (3, 4),
            (1, 5),
            (2, 5),
            (3, 5),
            (4, 5)
        };
        foreach ((int, int) edge in edges) {
            Color color1 = Edges[edge.Item1][edge.Item2];
            Color color2 = Edges[edge.Item2][edge.Item1];
            if (!adjacentEdges.ContainsKey((color1, color2))) {
                throw new InvalidCubeConfigurationException($"Edge cannot exist\n( {color1}, {color2} )");
            }
            adjacentEdges[(color1, color2)]++;
            if (adjacentEdges[(color1, color2)] > 1) {
                throw new InvalidCubeConfigurationException($"An edge cannot exist more than once\n( {color1}, {color2} ) appears {adjacentEdges[(color1, color2)]} times");
            }
        }
    }

    public void Rotate(Color face) {
        int index = (int) face - 1;
        Faces[index] = new() {
            Faces[index][5], Faces[index][3], Faces[index][0],
            Faces[index][6],                  Faces[index][1],
            Faces[index][7], Faces[index][4], Faces[index][2]
        };

        Color tmp;
        switch (face) {
        case Color.WHITE:
            tmp = Faces[1][2];
            Faces[1][2] = Faces[2][2];
            Faces[2][2] = Faces[3][2];
            Faces[3][2] = Faces[4][2];
            Faces[4][2] = tmp;
            
            tmp = Faces[1][1];
            Faces[1][1] = Faces[2][1];
            Faces[2][1] = Faces[3][1];
            Faces[3][1] = Faces[4][1];
            Faces[4][1] = tmp;

            tmp = Faces[1][0];
            Faces[1][0] = Faces[2][0];
            Faces[2][0] = Faces[3][0];
            Faces[3][0] = Faces[4][0];
            Faces[4][0] = tmp;

            break;
        
        case Color.ORANGE:
            tmp = Faces[4][7];
            Faces[4][7] = Faces[5][0];
            Faces[5][0] = Faces[2][0];
            Faces[2][0] = Faces[0][0];
            Faces[0][0] = tmp;

            tmp = Faces[4][4];
            Faces[4][4] = Faces[5][3];
            Faces[5][3] = Faces[2][3];
            Faces[2][3] = Faces[0][3];
            Faces[0][3] = tmp;

            tmp = Faces[4][2];
            Faces[4][2] = Faces[5][5];
            Faces[5][5] = Faces[2][5];
            Faces[2][5] = Faces[0][5];
            Faces[0][5] = tmp;
            
            break;
            
        case Color.GREEN:
            tmp = Faces[1][2];
            Faces[1][2] = Faces[5][0];
            Faces[5][0] = Faces[3][5];
            Faces[3][5] = Faces[0][7];
            Faces[0][7] = tmp;

            tmp = Faces[1][4];
            Faces[1][4] = Faces[5][1];
            Faces[5][1] = Faces[3][3];
            Faces[3][3] = Faces[0][6];
            Faces[0][6] = tmp;

            tmp = Faces[1][7];
            Faces[1][7] = Faces[5][2];
            Faces[5][2] = Faces[3][0];
            Faces[3][0] = Faces[0][5];
            Faces[0][5] = tmp;
            
            break;

        case Color.RED:
            tmp = Faces[4][0];
            Faces[4][0] = Faces[0][7];
            Faces[0][7] = Faces[2][7];
            Faces[2][7] = Faces[5][7];
            Faces[5][7] = tmp;

            tmp = Faces[4][3];
            Faces[4][3] = Faces[0][4];
            Faces[0][4] = Faces[2][4];
            Faces[2][4] = Faces[5][4];
            Faces[5][4] = tmp;

            tmp = Faces[4][5];
            Faces[4][5] = Faces[0][2];
            Faces[0][2] = Faces[2][2];
            Faces[2][2] = Faces[5][2];
            Faces[5][2] = tmp;
            
            break;

        case Color.BLUE:
            tmp = Faces[3][7];
            Faces[3][7] = Faces[5][5];
            Faces[5][5] = Faces[1][0];
            Faces[1][0] = Faces[0][2];
            Faces[0][2] = tmp;

            tmp = Faces[3][4];
            Faces[3][4] = Faces[5][6];
            Faces[5][6] = Faces[1][3];
            Faces[1][3] = Faces[0][1];
            Faces[0][1] = tmp;

            tmp = Faces[3][2];
            Faces[3][2] = Faces[5][7];
            Faces[5][7] = Faces[1][5];
            Faces[1][5] = Faces[0][0];
            Faces[0][0] = tmp;

            break;

        case Color.YELLOW:
            tmp = Faces[4][5];
            Faces[4][5] = Faces[3][5];
            Faces[3][5] = Faces[2][5];
            Faces[2][5] = Faces[1][5];
            Faces[1][5] = tmp;

            tmp = Faces[4][6];
            Faces[4][6] = Faces[3][6];
            Faces[3][6] = Faces[2][6];
            Faces[2][6] = Faces[1][6];
            Faces[1][6] = tmp;

            tmp = Faces[4][7];
            Faces[4][7] = Faces[3][7];
            Faces[3][7] = Faces[2][7];
            Faces[2][7] = Faces[1][7];
            Faces[1][7] = tmp;

            break;
        }
        Configure();
    }

    public enum Color {
        NONE, WHITE, ORANGE, GREEN, RED, BLUE, YELLOW
    }

    private Color _front = Color.GREEN;
    private Color _top = Color.WHITE;
    private List<List<Color>> _faces = new();
    private List<List<Color>> _edges = new();
    private List<List<List<Color>>> _corners = new();

    public Color Front {
        get { return _front; }
        set { _front = value; }
    }
    public Color Top {
        get { return _top; }
        set { _top = value; }
    }
    public List<List<Color>> Faces {
        get { return _faces; }
        set { _faces = value; }
    }
    public List<List<Color>> Edges {
        get { return _edges; }
        set { _edges = value; }
    }
    public List<List<List<Color>>> Corners {
        get { return _corners; }
        set { _corners = value; }
    }
}


[Serializable]
class InvalidCubeConfigurationException : Exception {
    public InvalidCubeConfigurationException() {}

    public InvalidCubeConfigurationException(string message)
    : base(message)
    {}
}