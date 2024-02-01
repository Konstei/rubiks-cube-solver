public class Program {
    public static void Main() {
        Cube cube = new(
            new List<Cube.Color> {
                Cube.Color.WHITE, Cube.Color.WHITE, Cube.Color.WHITE,
                Cube.Color.WHITE,                   Cube.Color.WHITE,
                Cube.Color.WHITE, Cube.Color.WHITE, Cube.Color.WHITE
            },
            new List<Cube.Color> {
                Cube.Color.ORANGE, Cube.Color.ORANGE, Cube.Color.ORANGE,
                Cube.Color.ORANGE,                    Cube.Color.ORANGE,
                Cube.Color.ORANGE, Cube.Color.ORANGE, Cube.Color.ORANGE
            },
            new List<Cube.Color> {
                Cube.Color.GREEN, Cube.Color.GREEN, Cube.Color.GREEN,
                Cube.Color.GREEN,                   Cube.Color.GREEN,
                Cube.Color.GREEN, Cube.Color.GREEN, Cube.Color.GREEN
            },
            new List<Cube.Color> {
                Cube.Color.RED, Cube.Color.RED, Cube.Color.RED,
                Cube.Color.RED,                 Cube.Color.RED,
                Cube.Color.RED, Cube.Color.RED, Cube.Color.RED
            },
            new List<Cube.Color> {
                Cube.Color.BLUE, Cube.Color.BLUE, Cube.Color.BLUE,
                Cube.Color.BLUE,                  Cube.Color.BLUE,
                Cube.Color.BLUE, Cube.Color.BLUE, Cube.Color.BLUE
            },
            new List<Cube.Color> {
                Cube.Color.YELLOW, Cube.Color.YELLOW, Cube.Color.YELLOW,
                Cube.Color.YELLOW,                    Cube.Color.YELLOW,
                Cube.Color.YELLOW, Cube.Color.YELLOW, Cube.Color.YELLOW
            }
        );
        
        /* foreach (Cube.Color color in Enum.GetValues(typeof(Cube.Color))) {
            Console.WriteLine($"{(int) color} {color}");
        } */
        Console.WriteLine($"{Cube.Color.WHITE}");
    }
}
