namespace space_prototype.Entities
{
    public class Collider3D
    {
        //TODO really expensive operation
        public bool Intersection(Entity e1, Entity e2)
        {
            for (var i = 0; i < e1.Model.Meshes.Count; i++)
            {
                var e1BoundingSphere = e1.Model.Meshes[i].BoundingSphere;
                e1BoundingSphere.Center += e1.Position;

                for (var j = 0; j < e2.Model.Meshes.Count; j++)
                {
                    var e2BoundingSphere = e2.Model.Meshes[j].BoundingSphere;
                    e2BoundingSphere.Center += e2.Position;

                    if (e1BoundingSphere.Intersects(e2BoundingSphere))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}