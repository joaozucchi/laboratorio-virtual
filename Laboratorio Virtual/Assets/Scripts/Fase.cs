using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public  class Fase 
{
    public  string pergunta, v1, v2, v3, vCorreta;

    public Fase() { }

    public Fase (string pergunta, string v1, string v2, string v3, string vCorreta)
    {
        this.pergunta = pergunta;
        this.v1 = v1;
        this.v3 = v2;
        this.v2 = v3;
        this.vCorreta = vCorreta;
    }
}
