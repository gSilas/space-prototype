using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.Entities
{
    public class BoxCollider3D
    {
        private BoundingBox _box;

        public BoundingBox Box
        {
            get { return _box; }
        }

        public BoxCollider3D(Model model)
        {
            _box = GenerateBoundingBox(model);
        }

        private BoundingBox GenerateBoundingBox(Model toCalculate)
        {
            Vector3 min = Vector3.Zero;
            Vector3 max = Vector3.Zero;
            //TODO work out how this works
            return new BoundingBox(min, max);
        }

        public bool Intersection(BoundingBox box)
        {
            return _box.Intersects(box); 
        }

        
    }
}