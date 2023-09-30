using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletMover : ManejoPallets 
{
    private InputConfig inputConfig;
    [SerializeField] public InputConfig inputConfigMobile;
    [SerializeField] public InputConfig inputConfigPC;

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;

    private void Start()
    {
#if UNITY_ANDROID
        inputConfig = inputConfigMobile;
#else
		inputConfig = inputConfigPC;
#endif
    }

    private void Update() 
    {
        if (!Tenencia() && Desde.Tenencia() && inputConfig.IsPressingLeft())
            PrimerPaso();

        if (Tenencia() && inputConfig.IsPressingDown())
            SegundoPaso();

        if(segundoCompleto && Tenencia() && inputConfig.IsPressingRight())    
            TercerPaso();

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

}
