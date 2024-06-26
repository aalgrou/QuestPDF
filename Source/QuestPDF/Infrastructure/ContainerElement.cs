﻿using System;
using System.Collections.Generic;
using QuestPDF.Drawing;
using QuestPDF.Elements;

namespace QuestPDF.Infrastructure
{
    internal abstract class ContainerElement : Element, IContainer
    {
        internal Element Child { get; set; } = Empty.Instance;

        IElement IContainer.Child
        {
            get => Child;
            set => Child = value as Element;
        }

        internal override IEnumerable<Element> GetChildren()
        {
            yield return Child;
        }

        internal override void CreateProxy(Func<Element, Element> create)
        {
            Child = create(Child);
        }

        internal override SpacePlan Measure(Size availableSpace)
        {
            var measurement = Child.Measure(availableSpace);
            
            if (measurement.Type == SpacePlanType.Wrap)
                return SpacePlan.Wrap("Forwarded from child");

            return measurement;
        }
        
        internal override void Draw(Size availableSpace)
        {
            Child?.Draw(availableSpace);
        }
    }
}