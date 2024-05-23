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

    public void Configure() {
        Edges = new() {
            new() { Color.NONE,  Faces[0][3], Faces[0][6], Faces[0][4], Faces[0][1], Color.NONE  },
            new() { Faces[1][1], Color.NONE,  Faces[1][4], Color.NONE,  Faces[1][3], Faces[1][6] },
            new() { Faces[2][1], Faces[2][3], Color.NONE,  Faces[2][4], Color.NONE,  Faces[2][6] },
            new() { Faces[3][1], Color.NONE,  Faces[3][3], Color.NONE,  Faces[3][4], Faces[3][6] },
            new() { Faces[4][1], Faces[4][4], Color.NONE,  Faces[4][3], Color.NONE,  Faces[4][6] },
            new() { Color.NONE,  Faces[5][3], Faces[5][1], Faces[5][4], Faces[5][6], Color.NONE  }
        };
        Corners = new() {
            new() { Faces[0][5], Faces[1][2], Faces[2][0] },
            new() { Faces[0][7], Faces[2][2], Faces[3][0] },
            new() { Faces[0][2], Faces[3][2], Faces[4][0] },
            new() { Faces[0][0], Faces[4][2], Faces[1][0] },
            new() { Faces[5][0], Faces[1][7], Faces[2][5] },
            new() { Faces[5][2], Faces[2][7], Faces[3][5] },
            new() { Faces[5][7], Faces[3][7], Faces[4][5] },
            new() { Faces[5][5], Faces[4][7], Faces[1][5] }
        };
    }

    // N. O.
    // EW
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
            throw new InvalidCubeException("Each color must appear exactly 9 times, including the center");
        }

        // check corners and corner twists
        int twists = 0;
        Dictionary<(Color, Color, Color), int> validCorners = new() {
            { ( Color.WHITE,  Color.ORANGE, Color.GREEN  ), 0 },
            { ( Color.WHITE,  Color.GREEN,  Color.RED    ), 0 },
            { ( Color.WHITE,  Color.RED,    Color.BLUE   ), 0 },
            { ( Color.WHITE,  Color.BLUE,   Color.ORANGE ), 0 },
            { ( Color.YELLOW, Color.ORANGE, Color.GREEN  ), 0 },
            { ( Color.YELLOW, Color.GREEN,  Color.RED    ), 0 },
            { ( Color.YELLOW, Color.RED,    Color.BLUE   ), 0 },
            { ( Color.YELLOW, Color.BLUE,   Color.ORANGE ), 0 }
        };
        foreach (List<Color> corner in Corners) {
            Color[] colors = { corner[0], corner[1], corner[2] };
            if (!validCorners.ContainsKey((colors[0], colors[1], colors[2]))) {
                throw new InvalidCubeException($"Corner cannot exist ( {colors[0]}, {colors[1]}, {colors[2]} )");
            }
            validCorners[(colors[0], colors[1], colors[2])]++;
            if (validCorners[(colors[0], colors[1], colors[2])] > 1) {
                throw new InvalidCubeException($"A corner cannot exist more than once, ( {colors[0]}, {colors[1]} ) appears {validCorners[(colors[0], colors[1], colors[2])]} times");
            }
            if (corner.Contains(Color.WHITE)) {
                if (corner[1] == Color.WHITE) twists++;
                else if (corner[2] == Color.WHITE) twists += 2;
            } else {
                if (corner[1] == Color.YELLOW) twists += 2;
                else if (corner[2] == Color.YELLOW) twists++;
            }
        }
        if (twists % 3 != 0) {
            throw new InvalidCubeException(
                "One or more of the corners is twisted. Please check your cube and try again.\n" +
                "Indications: Take two opposite colors and calculate the sum of the twists it takes for each corner to\n" +
                "    a) get one of those colors on the face with of the same color or on the face of its opposite color\n" +
                "    b) or to do the same to its opposite color if the first color is not present\n" +
                "If that sum modulo 3 is different than 0, the cube is unsolvable."
            );
        }

        // check color adjacency on all edges
        Dictionary<(Color, Color), int> validEdges = new() {
            { ( Color.WHITE,  Color.ORANGE ), 0 },
            { ( Color.WHITE,  Color.GREEN  ), 0 },
            { ( Color.WHITE,  Color.RED    ), 0 },
            { ( Color.WHITE,  Color.BLUE   ), 0 },
            { ( Color.ORANGE, Color.GREEN  ), 0 },
            { ( Color.ORANGE, Color.BLUE   ), 0 },
            { ( Color.ORANGE, Color.YELLOW ), 0 },
            { ( Color.GREEN,  Color.RED    ), 0 },
            { ( Color.GREEN,  Color.YELLOW ), 0 },
            { ( Color.RED,    Color.BLUE   ), 0 },
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
            (1, 5),
            (2, 3),
            (2, 5),
            (3, 4),
            (3, 5),
            (4, 5)
        };
        foreach ((int, int) edge in edges) {
            Color[] colors = { Edges[edge.Item1][edge.Item2], Edges[edge.Item2][edge.Item1] };
            Array.Sort(colors);
            if (!validEdges.ContainsKey((colors[0], colors[1]))) {
                throw new InvalidCubeException($"Edge cannot exist ( {colors[0]}, {colors[1]} )");
            }
            validEdges[(colors[0], colors[1])]++;
            if (validEdges[(colors[0], colors[1])] > 1) {
                throw new InvalidCubeException($"An edge cannot exist more than once, ( {colors[0]}, {colors[1]} ) appears {validEdges[(colors[0], colors[1])]} times");
            }
        }
    }

    public void RotateFace(Color face, bool conf = false) {
        int index = (int) face;
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
        Moves.Add(face);

        if (conf) Configure();
    }

    // i am most sure i won't need this one
    /* public void RotateSlice(Color face, bool skip = false) {
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
            break;
        case Color.GREEN:
            break;
        case Color.RED:
            break;
        case Color.BLUE:
            break;
        case Color.YELLOW:
            break;
        }

        if (skip) return;

        Configure();
    } */

    // i really believe i will not be needing this
    /* public void RotateCube(Color axis) {
        int index = (int) axis;
        switch (axis) {
        case Color.WHITE:
            RotateFace(Color.WHITE, skip: true);
            for (int i=0; i<3; i++) RotateFace(Color.YELLOW, skip: true);
            break;
        case Color.ORANGE:
            RotateFace(Color.ORANGE, skip: true);
            for (int i=0; i<3; i++) RotateFace(Color.RED, skip: true);
            break;
        case Color.GREEN:
            RotateFace(Color.GREEN, skip: true);
            for (int i=0; i<3; i++) RotateFace(Color.BLUE, skip: true);
            break;
        case Color.RED:
            RotateFace(Color.RED, skip: true);
            for (int i=0; i<3; i++) RotateFace(Color.ORANGE, skip: true);
            break;
        case Color.BLUE:
            RotateFace(Color.BLUE, skip: true);
            for (int i=0; i<3; i++) RotateFace(Color.GREEN, skip: true);
            break;
        case Color.YELLOW:
            RotateFace(Color.YELLOW, skip: true);
            for (int i=0; i<3; i++) RotateFace(Color.WHITE, skip: true);
            break;
        }
    } */

    public void Print() {
        Console.ResetColor();
        Console.Write("      ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[0][0]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[0][1]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[0][2]);
        Console.Write("  ");
        Console.WriteLine();
        Console.ResetColor();
        Console.ResetColor();
        Console.Write("      ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[0][3]);
        Console.Write("  ");
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[0][4]);
        Console.Write("  ");
        Console.WriteLine();
        Console.ResetColor();
        Console.ResetColor();
        Console.Write("      ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[0][5]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[0][6]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[0][7]);
        Console.Write("  ");
        Console.WriteLine();
        Console.ResetColor();
        
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[1][0]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[1][1]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[1][2]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[2][0]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[2][1]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[2][2]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[3][0]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[3][1]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[3][2]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[4][0]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[4][1]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[4][2]);
        Console.Write("  ");
        Console.WriteLine();
        Console.ResetColor();
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[1][3]);
        Console.Write("  ");
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[1][4]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[2][3]);
        Console.Write("  ");
        Console.BackgroundColor = ConsoleColor.Green;
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[2][4]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[3][3]);
        Console.Write("  ");
        Console.BackgroundColor = ConsoleColor.Red;
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[3][4]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[4][3]);
        Console.Write("  ");
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[4][4]);
        Console.Write("  ");
        Console.WriteLine();
        Console.ResetColor();
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[1][5]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[1][6]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[1][7]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[2][5]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[2][6]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[2][7]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[3][5]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[3][6]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[3][7]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[4][5]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[4][6]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[4][7]);
        Console.Write("  ");
        Console.WriteLine();
        Console.ResetColor();

        Console.ResetColor();
        Console.Write("      ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[5][0]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[5][1]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[5][2]);
        Console.Write("  ");
        Console.WriteLine();
        Console.ResetColor();
        Console.ResetColor();
        Console.Write("      ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[5][3]);
        Console.Write("  ");
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[5][4]);
        Console.Write("  ");
        Console.WriteLine();
        Console.ResetColor();
        Console.ResetColor();
        Console.Write("      ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[5][5]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[5][6]);
        Console.Write("  ");
        Console.BackgroundColor = GetColorAsConsoleColor(Faces[5][7]);
        Console.Write("  ");
        Console.WriteLine();
        Console.ResetColor();
    }

    private ConsoleColor GetColorAsConsoleColor(Color color) {
        switch (color) {
            case Color.WHITE:  return ConsoleColor.White;
            case Color.ORANGE: return ConsoleColor.DarkYellow;
            case Color.GREEN:  return ConsoleColor.Green;
            case Color.RED:    return ConsoleColor.Red;
            case Color.BLUE:   return ConsoleColor.Blue;
            case Color.YELLOW: return ConsoleColor.Yellow;
            default: return ConsoleColor.Black;
        }
    }

    public enum Color {
        NONE=-1, WHITE, ORANGE, GREEN, RED, BLUE, YELLOW
    }

    private Color _front = Color.GREEN;
    private Color _top = Color.WHITE;
    private List<List<Color>> _faces = new();
    private List<List<Color>> _edges = new();
    private List<List<Color>> _corners = new();

    private List<Color> _moves = new();

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
    public List<List<Color>> Corners {
        get { return _corners; }
        set { _corners = value; }
    }

    public List<Color> Moves {
        get { return _moves; }
        set { _moves = value; }
    }
}


[Serializable]
class InvalidCubeException : Exception {
    public InvalidCubeException() {}

    public InvalidCubeException(string message)
    : base(message)
    {}
}