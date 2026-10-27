# Turkish Chat Support

FFXIV's font has no ğ, Ğ, ş, ı or İ. Type them in chat and every player sees `=` instead. This Dalamud plugin swaps those five letters for ones the font has, so your messages read fine for everyone, whether they use the plugin or not.

| You type | Others see |
|---|---|
| ğ Ğ | g G |
| ş | s |
| ı | i, or Greek ι if you pick it in the settings |
| İ | I |

ç, ö, ü and Ş are already in the font and stay as they are.

## What it fixes

- Letters you type into any game text box change as you type them.
- Anything you send from the chat box is fixed right before it goes out, pasted text included.
- Messages and speech bubbles from Turkish players who don't have the plugin show letters instead of `=`. This part only changes your own screen.

Text pasted into other boxes, such as a party finder comment, is not fixed.

## Install

1. In game, type `/xlsettings` and open the Experimental tab.
2. Under Custom Plugin Repositories, paste `https://raw.githubusercontent.com/ceeprus/ffxiv-turkish-chat/main/repo.json` into the empty row, click +, then the save icon.
3. Type `/xlplugins`, search for Turkish Chat Support and install it.

The settings open from the cog next to the plugin in the installer.

## Türkçe

Oyunun yazı tipinde ğ, Ğ, ş, ı ve İ yok. Sohbete yazınca herkes bu harflerin yerinde `=` görüyor. Bu Dalamud eklentisi eksik beş harfi yazı tipinde olan harflerle değiştirir: ğ yerine g, Ğ yerine G, ş yerine s, ı yerine i (ayarlardan Yunan harfi ι da seçilebilir), İ yerine I. Böylece mesajların eklentisi olmayan oyunculara da okunaklı ulaşır.

- Oyundaki metin kutularına yazdığın harfler, sen yazarken değişir.
- Sohbet kutusundan gönderdiğin her şey, yapıştırdığın metin de dahil, gönderilmeden hemen önce düzeltilir.
- Eklentisi olmayan Türk oyuncuların mesajları ve konuşma balonları senin ekranında `=` yerine harflerle görünür. Bu kısım sadece senin ekranını değiştirir.

ç, ö, ü ve Ş zaten yazı tipinde var, onlara dokunulmaz. Party Finder açıklaması gibi başka kutulara yapıştırılan metin düzeltilmez.

### Kurulum

1. Oyunda `/xlsettings` yaz, Experimental sekmesini aç.
2. Custom Plugin Repositories altındaki boş satıra `https://raw.githubusercontent.com/ceeprus/ffxiv-turkish-chat/main/repo.json` adresini yapıştır, + düğmesine, sonra kaydet simgesine bas.
3. `/xlplugins` yaz, Turkish Chat Support'u bul ve kur.

Ayarlar, eklenti listesinde eklentinin yanındaki dişli simgesinden açılır.
