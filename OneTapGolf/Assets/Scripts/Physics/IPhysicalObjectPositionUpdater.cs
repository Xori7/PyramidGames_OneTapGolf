namespace OneTapGolf.Physics {
    public interface IPhysicalObjectPositionUpdater {
        ///<summary>
        ///Sets physical object position to new one that object has when "timeElapsed" past, base on implemented physic system
        ///</summary>
        void UpdatePhysicalObjectPosition(float timeElapsed);
    }
}