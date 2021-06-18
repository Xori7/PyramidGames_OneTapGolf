namespace OneTapGolf.Physics {
    public sealed class DefaultPhysicalObjectPositionUpdater : IPhysicalObjectPositionUpdater {
        private readonly PhysicalObject physicalObject;

        public DefaultPhysicalObjectPositionUpdater(PhysicalObject physicalObject) {
            this.physicalObject = physicalObject ?? throw new System.ArgumentNullException(nameof(physicalObject));
        }

        public void UpdatePhysicalObjectPosition(float timeElapsed) {
            physicalObject.position += physicalObject.velocity * timeElapsed;
        }
    }
}