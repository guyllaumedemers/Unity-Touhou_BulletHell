using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonComponent : ButtonComponent
{
    private void Awake()
    {
        RegisterOperation(new BlinkingTextDecorator(this));
    }
}
