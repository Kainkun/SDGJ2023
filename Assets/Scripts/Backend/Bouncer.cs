using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public Bar BarRef;
    public Line LineRef;
    public int Guess; // The Player's guess

    public void Admit(Patron p) {
        BarRef.Enter(p);
    }

    public void Reject() {
        LineRef.Reject();
    }

    public void Interact(Patron p) {
        
    }

    public void ChangeGuess(int val) {
        Guess += val;
    }

}