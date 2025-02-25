public class Cube {
    public Cube () {
        Faces = new() {
            new List<Color> {
                Color.WHITE, Color.WHITE, Color.WHITE,
                Color.WHITE,                   Color.WHITE,
                Color.WHITE, Color.WHITE, Color.WHITE
            },
            new List<Color> {
                Color.ORANGE, Color.ORANGE, Color.ORANGE,
                Color.ORANGE,                    Color.ORANGE,
                Color.ORANGE, Color.ORANGE, Color.ORANGE
            },
            new List<Color> {
                Color.GREEN, Color.GREEN, Color.GREEN,
                Color.GREEN,                   Color.GREEN,
                Color.GREEN, Color.GREEN, Color.GREEN
            },
            new List<Color> {
                Color.RED, Color.RED, Color.RED,
                Color.RED,                 Color.RED,
                Color.RED, Color.RED, Color.RED
            },
            new List<Color> {
                Color.BLUE, Color.BLUE, Color.BLUE,
                Color.BLUE,                  Color.BLUE,
                Color.BLUE, Color.BLUE, Color.BLUE
            },
            new List<Color> {
                Color.YELLOW, Color.YELLOW, Color.YELLOW,
                Color.YELLOW,                    Color.YELLOW,
                Color.YELLOW, Color.YELLOW, Color.YELLOW
            }
        };
        Configure();
        // Validate();
    }

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
        // Validate();
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
            throw new InvalidCubeException("Each color must appear exactly 9 times");
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
        foreach ((int, int) edge in validEdges.Keys) {
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


        // corners are always (white/yellow, face with min index, face with max index)
        // but yellow ones are reversed relative to white, so white on bottom or yellow on top has to use reverse color orders
        // check top 4: if white, use normal dict; if yellow, use reverse dict
        // check bottom 4: if yellow, use normal dict; if white, use reverse dict
        Dictionary<(Color, Color, Color), int> validCornersDefault = new() {
            { ( Color.WHITE,  Color.ORANGE, Color.GREEN  ), 0 },
            { ( Color.WHITE,  Color.GREEN,  Color.RED    ), 0 },
            { ( Color.WHITE,  Color.RED,    Color.BLUE   ), 0 },
            { ( Color.WHITE,  Color.BLUE,   Color.ORANGE ), 0 },
            { ( Color.YELLOW, Color.ORANGE, Color.GREEN  ), 0 },
            { ( Color.YELLOW, Color.GREEN,  Color.RED    ), 0 },
            { ( Color.YELLOW, Color.RED,    Color.BLUE   ), 0 },
            { ( Color.YELLOW, Color.BLUE,   Color.ORANGE ), 0 }
        };
        Dictionary<(Color, Color, Color), int> validCornersReversed = new() {
            { ( Color.WHITE,  Color.GREEN,  Color.ORANGE ), 0 },
            { ( Color.WHITE,  Color.RED,    Color.GREEN  ), 0 },
            { ( Color.WHITE,  Color.BLUE,   Color.RED    ), 0 },
            { ( Color.WHITE,  Color.ORANGE, Color.BLUE   ), 0 },
            { ( Color.YELLOW, Color.GREEN,  Color.ORANGE ), 0 },
            { ( Color.YELLOW, Color.RED,    Color.GREEN  ), 0 },
            { ( Color.YELLOW, Color.BLUE,   Color.RED    ), 0 },
            { ( Color.YELLOW, Color.ORANGE, Color.BLUE   ), 0 }
        };

        int twists=0;
        List<Color> corner;
        Color tempColor;

        for (int i=0; i<4; i++) {
            corner = new(Corners[i]);
            while (corner[0] != Color.WHITE && corner[0] != Color.YELLOW) {
                tempColor = corner[0];
                corner[0]=corner[1];
                corner[1]=corner[2];
                corner[2]=tempColor;
                twists++;
            }
            if (corner[0] == Color.WHITE) {
                if (!validCornersDefault.ContainsKey((corner[0], corner[1], corner[2]))) {
                    throw new InvalidCubeException($"Corner cannot exist: ( {Corners[i][0]}, {Corners[i][1]}, {Corners[i][2]} )");
                }
                validCornersDefault[(corner[0], corner[1], corner[2])]++;
            } else {
                if (!validCornersReversed.ContainsKey((corner[0], corner[1], corner[2]))) {
                    throw new InvalidCubeException($"Corner cannot exist: ( {Corners[i][0]}, {Corners[i][1]}, {Corners[i][2]} )");
                }
                validCornersReversed[(corner[0], corner[1], corner[2])]++;
            }
        }
        for (int i=4; i<8; i++) {
            corner = new(Corners[i]);
            while (corner[0] != Color.WHITE && corner[0] != Color.YELLOW) {
                tempColor = corner[2];
                corner[2]=corner[1];
                corner[1]=corner[0];
                corner[0]=tempColor;
                twists++;
            }
            if (corner[0] == Color.YELLOW) {
                if (!validCornersDefault.ContainsKey((corner[0], corner[1], corner[2]))) {
                    throw new InvalidCubeException($"Corner cannot exist: ( {Corners[i][0]}, {Corners[i][1]}, {Corners[i][2]} )");
                }
                validCornersDefault[(corner[0], corner[1], corner[2])]++;
            } else {
                if (!validCornersReversed.ContainsKey((corner[0], corner[1], corner[2]))) {
                    throw new InvalidCubeException($"Corner cannot exist: ( {Corners[i][0]}, {Corners[i][1]}, {Corners[i][2]} )");
                }
                validCornersReversed[(corner[0], corner[1], corner[2])]++;
            }
        }

        if (twists % 3 != 0) {
            throw new InvalidCubeException (
                "Corner twist detected: One or more corners are misaligned, making the cube unsolvable.\n" +
                "To verify: For each corner, count the twists needed to align its colors with their respective faces.\n" +
                "If the total twists (modulo 3) are not zero, the cube cannot be solved.\n" +
                "Please check and adjust your cube, then try again."
            );
        }



        // permutation parity
        // how many swaps does it take to bring all the pieces back to their default position
        // for corners i:0->7, j:i+1->8
        // for edges, put them in a single list
        // first copy the pieces so that i can move them around later on

        int permutations = 0;
        List<List<Color>> currentCorners = Corners.Select(inner => inner.OrderBy(n => n).ToList()).ToList();
        List<List<Color>> defaultCorners = validCornersDefault.Keys.ToList()
                                            .Select(inner => new List<Color> { inner.Item1, inner.Item2, inner.Item3 }
                                                                    .OrderBy(n => n)
                                                                    .ToList())
                                            .ToList();
        for (int i=0; i<7; i++) {
            if (currentCorners[i][0] == defaultCorners[i][0] && currentCorners[i][1] == defaultCorners[i][1] && currentCorners[i][2] == defaultCorners[i][2]) continue;
            int position = -1;
            for (int j=i+1; j<8; j++) {
                if (currentCorners[j][0] == defaultCorners[i][0] && currentCorners[j][1] == defaultCorners[i][1] && currentCorners[j][2] == defaultCorners[i][2]) {
                    position = j;
                    break;
                }
            }
            List<Color> tempCorner = currentCorners[i];
            currentCorners[i] = currentCorners[position];
            currentCorners[position] = tempCorner;
            permutations++;
        }
        List<List<Color>> currentEdges = new List<List<Color>> {
            new() { Edges[0][1], Edges[1][0] },
            new() { Edges[0][2], Edges[2][0] },
            new() { Edges[0][3], Edges[3][0] },
            new() { Edges[0][4], Edges[4][0] },
            new() { Edges[1][2], Edges[2][1] },
            new() { Edges[2][3], Edges[3][2] },
            new() { Edges[3][4], Edges[4][3] },
            new() { Edges[4][1], Edges[1][4] },
            new() { Edges[5][1], Edges[1][5] },
            new() { Edges[5][2], Edges[2][5] },
            new() { Edges[5][3], Edges[3][5] },
            new() { Edges[5][4], Edges[4][5] }
        }.Select(inner => inner.OrderBy(n => n).ToList()).ToList();
        List<List<Color>> defaultEdges = new() {
            new() { Color.WHITE,  Color.ORANGE },
            new() { Color.WHITE,  Color.GREEN  },
            new() { Color.WHITE,  Color.RED    },
            new() { Color.WHITE,  Color.BLUE   },
            new() { Color.ORANGE, Color.GREEN  },
            new() { Color.GREEN,  Color.RED    },
            new() { Color.RED,    Color.BLUE   },
            new() { Color.ORANGE, Color.BLUE   },
            new() { Color.ORANGE, Color.YELLOW },
            new() { Color.GREEN,  Color.YELLOW },
            new() { Color.RED,    Color.YELLOW },
            new() { Color.BLUE,   Color.YELLOW }
        };
        for (int i=0; i<11; i++) {
            if (currentEdges[i][0] == defaultEdges[i][0] && currentEdges[i][1] == defaultEdges[i][1]) continue;
            int position = -1;
            for (int j=i+1; j<12; j++) {
                if (currentEdges[j][0] == defaultEdges[i][0] &&
                    currentEdges[j][1] == defaultEdges[i][1]
                ) {
                    position = j;
                    break;
                }
            }
            List<Color> tempEdge = currentEdges[i];
            currentEdges[i] = currentEdges[position];
            currentEdges[position] = tempEdge;
            permutations++;
        }
        if (permutations % 2 == 1) {
            throw new InvalidCubeException(
                "Permutation parity error detected: The cube's piece permutation is inconsistent, making it unsolvable.\n" +
                "To verify: Count the number of swaps needed to restore a valid permutation of all pieces.\n" +
                "If the total number of swaps is odd, the cube cannot be solved in a legal state.\n" +
                "Please check for incorrect sticker placements or reassembly errors, then try again."
            );
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