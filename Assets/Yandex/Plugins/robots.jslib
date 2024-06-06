mergeInto(LibraryManager.library, {

    GetPlayerData: function () {

      //  console.Log(player.getName());
    },


    OutputDebug: function () {
   // window.alert("Hello, world!");
  // console.Log("Hello, world!");
},


HelloString: function (str) {
   // window.alert(UTF8ToString(str));
},


ShowAdv : function () {

    myGameInstance.SendMessage('GameAudioController', 'DisableAllSounds');

    ysdk.adv.showFullscreenAdv({
        callbacks: {
            onClose: function(wasShown) {
              myGameInstance.SendMessage('GameAudioController', 'EnableAllSounds');
          },
          onError: function(error) {
              myGameInstance.SendMessage('GameAudioController', 'EnableAllSounds');
          }
      }
  })
},

RateUs: function () {

 ysdk.feedback.canReview()
 .then(({ value, reason }) => {
    if (value) {
        ysdk.feedback.requestReview()
        .then(({ feedbackSent }) => {
           // console.log(feedbackSent);
        })
    } else {
       // console.log(reason)
    }
})
},

CheckRateUs: function () {

 ysdk.feedback.canReview()
 .then(({ value, reason }) => {
    if (value)
    {
       // console.log('can show rateus.');
        myGameInstance.SendMessage('MainMenu', 'SetStatusRateUs');
        //открыть окно оценки рейтинга
      //  return 1;
    } 
    else 
    {
      //  console.log(reason);
       // return 0;
    }
})
},



GetCurentLang: function () {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
},


ShowRewardVideoForSkin: function (str) {

 var valueStr = UTF8ToString(str); 
 ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          //console.log('Video ad open.');
      },
      onRewarded: () => {          
          myGameInstance.SendMessage('MainMenu', 'GetSkinForAdvertising', valueStr);

          //console.log('Rewarded!');
      },
      onClose: () => {
          myGameInstance.SendMessage('AudioController', 'PlaySoundOpenItem');
          myGameInstance.SendMessage('AudioController', 'PlayBackgroundMenu');
      }, 
      onError: (e) => {
          //console.log('Error while open video ad:', e);
      }
  }
})
},


ShowRewardVideoForWeapon: function (str) {

 var valueStr = UTF8ToString(str); 
 ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          //console.log('Video ad open.');
      },
      onRewarded: () => {          
          myGameInstance.SendMessage('MainMenu', 'GetWeaponForAdvertising', valueStr);

          //console.log('Rewarded!');
      },
      onClose: () => {
          myGameInstance.SendMessage('AudioController', 'PlaySoundOpenItem');
          myGameInstance.SendMessage('AudioController', 'PlayBackgroundMenu');
      }, 
      onError: (e) => {
          //console.log('Error while open video ad:', e);
      }
  }
})
},

BuySkin : function (str) {

    var valueStr = UTF8ToString(str);

    payments.purchase({ id: valueStr }).then(purchase => {
        // Покупка успешно совершена!
       //console.log('has skin.');
        myGameInstance.SendMessage('MainMenu', 'GetSkinForever', valueStr);
    }).catch(err => {
        // Покупка не удалась: в консоли разработчика не добавлен товар с таким id,
        // пользователь не авторизовался, передумал и закрыл окно оплаты,
        // истекло отведенное на покупку время, не хватило денег и т. д.
       // myGameInstance.SendMessage('MainMenu', 'EmptyMethod');
       // myGameInstance.SendMessage('AudioController', 'PlayBackgroundMenu');
       window.focus();
       myGameInstance.SendMessage('AudioController', 'PlayBackgroundMenu');
    })

},

BuyWeapon : function (str) {

    var valueStr = UTF8ToString(str);

    payments.purchase({ id: valueStr }).then(purchase => {
       
        myGameInstance.SendMessage('MainMenu', 'GetWeaponForever', valueStr);
    }).catch(err => {
        // Покупка не удалась: в консоли разработчика не добавлен товар с таким id,
        // пользователь не авторизовался, передумал и закрыл окно оплаты,
        // истекло отведенное на покупку время, не хватило денег и т. д.
        window.focus();
        myGameInstance.SendMessage('AudioController', 'PlayBackgroundMenu');
    })

},

CheckPurchase : function (str) {


    var valueStr = UTF8ToString(str);
    payments.getPurchases().then(purchases => {

       if (purchases.some(purchase => purchase.productID === valueStr)) 
        {
            //метод открытия
           // console.log(valueStr);                           
            myGameInstance.SendMessage('Progress', 'AddAvailableItems', valueStr);

        }
    }).catch(err => {
        // Выбрасывает исключение USER_NOT_AUTHORIZED для неавторизованных пользователей.
        //return 0;
    })

},


SetLevel : function (data) {

   var dataString = UTF8ToString(data);
   var myobj = JSON.parse(dataString);
   player.setData(myobj);

 //  player.setStats({
  //      valueKey: level,
  //  }).then(() => {
  //      console.log(valueKey);
  //      console.log(level);
  //  });
},

GetLevel : function () {

player.getData().then(_date => {
    const myJSON = JSON.stringify(_date);
    myGameInstance.SendMessage('Progress', 'LoadData', myJSON)
});
   // var amount = player.getStats()
   // console.log(amount);
   // return amount;
},

GetAllPurchase : function () {



    payments.getPurchases().then(purchases => {

        var th = purchases;
        

        //if (purchases.some(purchase => purchase.productID === valueStr)) 
       // {
            //метод открытия
            //console.log(valueStr);
           // return 1;

       // }
    }).catch(err => {
        // Выбрасывает исключение USER_NOT_AUTHORIZED для неавторизованных пользователей.
       // return 0;
    })

},

ReloadPage : function () {

    location.reload();

},


ShowRewardVideoForContinue: function () {

 ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          //console.log('Video ad open.');
      },
      onRewarded: () => {          
          myGameInstance.SendMessage('GameController', 'ContinueGameAfrerReward');

          //console.log('Rewarded!');
      },
      onClose: () => {
          myGameInstance.SendMessage('GameAudioController', 'EnableAllSounds');
          myGameInstance.SendMessage('GameController', 'CloseRewardedVideo');
      }, 
      onError: (e) => {
          //console.log('Error while open video ad:', e);
      }
  }
})
},


CheckDeviceInfo: function () {

 if(ysdk.deviceInfo.isDesktop())
 {
    
    return 1;

 }
 else
 {
    
    return 0;

 }
},

GetCurentTLD: function () {
    var lang = ysdk.environment.i18n.tld;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
},



});