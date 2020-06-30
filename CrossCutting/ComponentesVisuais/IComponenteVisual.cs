using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CrossCutting.ComponentesVisuais
{
    public interface IComponenteVisual<T>
    {
        T WithColor(Color color);
        T WithColor(double red, double green, double blue);
        T WithSize(float size);
    }
}
