public class Solver {
    public Solver(Cube cube) { _cube = cube; }

    private int CornerTwists(int face1, int face2, int face3) {
        int[] cornerFaces = { face1, face2, face3 };
        Array.Sort(cornerFaces);
        // placeholder
        return 0;
    }

    private Cube _cube;
}