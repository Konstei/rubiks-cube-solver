using static Cube;

public static class WhiteCross {
    public static void Solve(Cube cube) {
        foreach (List<Color> face in cube.Faces) {
            foreach (Color color in face) {
                Console.Write($"{color} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
