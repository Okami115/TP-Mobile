using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletMover : ManejoPallets {

    public MoveType miInput;
    public enum MoveType {
        WASD,
        Arrows,
        Mobile
    }

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;

    private bool isLeftPress = false;
    private bool isRightPress = false;

    private void Update() 
    {
        Debug.Log("Botton: " + isLeftPress);
        switch (miInput) {
            case MoveType.WASD:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.A)) {
                    PrimerPaso();
                }
                if (Tenencia() && Input.GetKeyDown(KeyCode.S)) {
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.D)) {
                    TercerPaso();
                }
                break;

            case MoveType.Arrows:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.LeftArrow)) {
                    PrimerPaso();
                }
                if (Tenencia() && Input.GetKeyDown(KeyCode.DownArrow)) {
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.RightArrow)) {
                    TercerPaso();
                }
                break;

            case MoveType.Mobile:
                 Debug.Log("Botton: " + isLeftPress);
                if (!Tenencia() && Desde.Tenencia() && isLeftPress)
                {
                    PrimerPaso();
                }
                if (Tenencia() && isRightPress)
                {
                    SegundoPaso();
                    TercerPaso();
                }
                break;

            default:
                break;
        }

        Debug.Log("Botton: " + isLeftPress);
    }

    void PrimerPaso() {
        Desde.Dar(this);
        segundoCompleto = false;
    }
    void SegundoPaso() {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    void TercerPaso() {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor) {
        if (Tenencia()) {
            if (receptor.Recibir(Pallets[0])) {
                Pallets.RemoveAt(0);
            }
        }
    }
    public override bool Recibir(Pallet pallet) {
        if (!Tenencia()) {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }

    public void ClickLeft()
    {
        isLeftPress = true;
        Debug.Log("Botton: " + isLeftPress);

    }

    public void ClickRight()
    {
        isRightPress = true;
    }

    public void releaseLeft()
    {
        isLeftPress = false;
        Debug.Log("Click");
    }

    public void releaseRight()
    {
        isRightPress = false;
    }
}
