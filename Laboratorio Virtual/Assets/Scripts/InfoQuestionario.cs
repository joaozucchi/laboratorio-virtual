using UnityEngine.UI;
using System;
[Serializable]
public class InfoQuestionario
{
    public string uidAutor, nomeQuestionario;
    public int n;
    public InfoQuestionario() { }

    public InfoQuestionario (string uidAutor, string nomeQuestionario, int n)
    {
        this.nomeQuestionario = nomeQuestionario;
        this.uidAutor = uidAutor;
        this.n = n;
    }
}
