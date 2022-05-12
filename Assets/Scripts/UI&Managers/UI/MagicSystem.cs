using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MagicSystem
{
    #region Variables
    public const int MAX_FRAGMENT_AMOUNT = 4;
    public event EventHandler OnUsed;
    public event EventHandler OnRecovered;
    public event EventHandler OnEmpty;
    private List<Magic> magicList;
    #endregion

    #region Methods

    public MagicSystem(int magicAmount)
    {
        magicList = new List<Magic>();
        for (int i=0; i<magicAmount; i++)
        {
            Magic magic = new Magic(4);
            magicList.Add(magic);
        }
    }

    public void Use(int useAmount)
    {
        //cycle through all hearts starting form the end
        for (int i=magicList.Count-1; i >= 0; i--)
        {
            Magic magic = magicList[i];
            //test is this heart can absorb damageAmount
            if (useAmount > magic.Fragments)
            {
                //heart cannot absorb full damage, damage heart then keep going to next heart
                useAmount -= magic.Fragments;
                magic.Use(magic.Fragments);
            }
            else
            {
                //heart can absorb sull damage, then break out of cycle
                magic.Use(useAmount);
                break;
            }
        }

        if (OnUsed != null) OnUsed(this, EventArgs.Empty);

        if (IsEmpty())
            if (OnEmpty != null) OnEmpty(this, EventArgs.Empty);
    }

    public void Recover(int recoverAmount)
    {
        for (int i=0; i < magicList.Count; i++)
        {
            Magic magic = magicList[i];
            int missingFragments = MAX_FRAGMENT_AMOUNT - magic.Fragments;
            if (recoverAmount > missingFragments)
            {
                recoverAmount -= missingFragments;
                magic.Recover(missingFragments);
            }
            else
            {
                magic.Recover(recoverAmount);
                break;
            }
        }

        if (OnRecovered != null) OnRecovered(this, EventArgs.Empty);
    }

    public bool IsEmpty()
    {
        return magicList[0].Fragments == 0;
    }

    public List<Magic> MagicList
    {
        get { return magicList; }
    }

    //represents a single heart
    public class Magic 
    {
        private int fragments;

        public Magic(int fragments)
        {
            this.fragments = fragments;
        }

        public void Use(int useAmt)
        {
            if (useAmt >= fragments)
            {
                fragments = 0;
            }
            else
            {
                fragments -= useAmt;
            }
        }

        public void Recover(int recoverAmt)
        {
            if (fragments + recoverAmt > MAX_FRAGMENT_AMOUNT)
            {
                fragments = MAX_FRAGMENT_AMOUNT;
            }
            else
            {
                fragments += recoverAmt;
            }
        }

        public int Fragments
        {
            get { return fragments; }
            set { fragments = value; }
        }
    }
    #endregion
}
