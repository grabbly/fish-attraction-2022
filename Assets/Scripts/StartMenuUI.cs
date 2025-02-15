using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StartMenuUI : MonoBehaviour
{
   [SerializeField] private Transform fishPointPanel;
   [SerializeField] private TMP_Text coins;
   [SerializeField] private int addTimeAmount = 5;

   [SerializeField] private TMP_Text addTimeText;
   [SerializeField] private TMP_Text addTimeCost;
   [SerializeField] private TMP_Text addFishCost;
   [SerializeField] private TMP_Text fishAmount;
   [SerializeField] private StringUnityEvent onTimeChanged;
   [SerializeField] private UnityEvent onTimeAdded;
   [SerializeField] private IntUnityEvent onMoreFishAdded;

   private int addTimeMultiplicator
   {
      get => Model.addTimeMultiplicator;
      set => Model.addTimeMultiplicator = value;
   }

   private int addFishMultiplicator
   {
      get => Model.addFishMultiplicator;
      set => Model.addFishMultiplicator = value;
   }

   private int addFishAmount = 1;

   // [SerializeField] private StringUnityEvent onFPChanged;
   public void Init()
   {
      // addFishMultiplicator = 1;
      // addTimeMultiplicator = 1;
      InitTextsAndCost();
   }

   private void InitTextsAndCost()
   {
      coins.text = Model.FishPoints.ToString();
      onTimeChanged.Invoke(Model.GameTime.ToString());
      addTimeCost.text = (addTimeMultiplicator * addTimeAmount * Model.excangeRateFPtoTime).ToString();
      addTimeText.text = $"+{(addTimeAmount).ToString()} SEC";
      addFishCost.text = (addFishMultiplicator * addFishAmount).ToString();
      fishAmount.text = "Now: "+Model.FishAmount.ToString();
   }

   public void AddMoreFish()
   {
      var cost =  addFishMultiplicator * addFishAmount;
      if (Model.FishPoints >= cost)
      {
         onMoreFishAdded.Invoke(addFishAmount);
         Model.FishAmount += addFishAmount;
         Model.FishPoints -= cost;
         coins.text = Model.FishPoints.ToString();
         .5f.Delay(()=>
         {
            InitTextsAndCost();
         });
         
         addFishMultiplicator *= 2;
      }else
      {
         fishPointPanel.DOShakePosition(1, 10);
      }
   }

   public void AddTime()
   {
      var cost = addTimeMultiplicator * addTimeAmount * Model.excangeRateFPtoTime;
      if (Model.FishPoints >= cost)
      {
         onTimeAdded.Invoke();
         Model.GameTime += addTimeAmount;
         Model.FishPoints -= cost;
         coins.text = Model.FishPoints.ToString();
         1f.Delay(()=>
         {
            onTimeChanged.Invoke(Model.GameTime.ToString());
            InitTextsAndCost();
         });
         
         addTimeMultiplicator *= 2;
         
      }
      else
      {
         fishPointPanel.DOShakePosition(1, 10);
      }
   }
}
