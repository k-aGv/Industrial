
namespace kagv.DLL_source {
    public class Util {
        public static DiagonalMovement GetDiagonalMovement(bool iCrossCorners, bool iCrossAdjacentPoint) {

            if (iCrossCorners && iCrossAdjacentPoint) 
                return DiagonalMovement.Always;
            return iCrossCorners ? DiagonalMovement.IfAtLeastOneWalkable : DiagonalMovement.OnlyWhenNoObstacles;
        }
    }
}
