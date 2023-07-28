using SpawnFactory.Triangles;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpawnFactory.UnityExtensions
{
    public static class UnityExtensions
    {
        /// <summary>
        ///  Extension method to check if a layer is in a layermask
        /// </summary>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        /// <summary>
        /// Extends all arrays with a method for adding on a second array
        /// </summary>
        public static T[] Concatenate<T>(this T[] first, T[] second)
        {
            if (first == null)
            {
                return second;
            }
            if (second == null)
            {
                return first;
            }

            return first.Concat(second).ToArray();
        }

        /// <summary>
        /// Extends List of Vector3 to convert vertices to triangles
        /// </summary>
        /// <param name="verticies">The vertices to convert into triangles</param>
        /// <returns>Returns a List of Triangles</returns>
        public static List<Triangle> Triangulate(this List<Vector3> verticies)
        {
            List<Vector2> vertices2D = new List<Vector2>();
            for (int i = 0; i < verticies.Count; i++)
                vertices2D.Add(new Vector2(verticies[i].x, verticies[i].z));

            Triangulator tr = new Triangulator(vertices2D.ToArray());
            int[] indicies = tr.Triangulate();

            List<Triangle> tris = new List<Triangle>();
            for (int i = 0; i < indicies.Length; i += 3)
            {
                Triangle newTri = new Triangle(verticies[indicies[i]], verticies[indicies[i + 1]], verticies[indicies[i + 2]]);
                tris.Add(newTri);
            }
            return tris;
        }
    }
}