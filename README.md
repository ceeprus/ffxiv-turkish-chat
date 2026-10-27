# Turkish Chat Support

FFXIV's chat font has no ğ, Ğ, ş, ı or İ. Type them in chat and every player sees `=` instead. This Dalamud plugin swaps those five letters for ones the font has, so your messages read fine for everyone, whether they use the plugin or not.

| You type | Others see |
|---|---|
| ğ Ğ | g G |
| ş | s |
| ı | i, or Greek ι if you pick it in the settings |
| İ | I |

ç, Ç, ö, Ö, ü, Ü and Ş are already in the font and stay as they are.

## What it fixes

- Letters you type into any game text box change as you type them.
- The plugin fixes anything you send from the chat box right before it goes out, pasted text included. Pasted text still shows `=` in the box until you send it.
- Messages and speech bubbles from Turkish players who don't have the plugin show letters instead of `=`. This part only changes your own screen. The i or ι choice applies here too.

The plugin does not fix text pasted into other boxes, such as a Party Finder comment.

## Install

1. In game, type `/xlsettings` and open the Experimental tab.
2. Under Custom Plugin Repositories, read the warning and click "Ok, I have read and understood this warning" when the 15-second wait ends (first time only). Paste `https://raw.githubusercontent.com/ceeprus/ffxiv-turkish-chat/main/repo.json` into the empty row, click +, then click Save at the bottom right.
3. Type `/xlplugins`, search for Turkish Chat Support and install it.

To open the settings, go to Installed Plugins in `/xlplugins` and click Settings (the cog) on Turkish Chat Support.

## Türkçe

Oyunun yazı tipinde ğ, Ğ, ş, ı ve İ yok. Sohbete yazınca herkes bu harflerin yerinde `=` görüyor. Bu Dalamud eklentisi eksik beş harfi yazı tipinde olan harflerle değiştirir: ğ yerine g, Ğ yerine G, ş yerine s, ı yerine i (ayarlardan Yunan alfabesindeki ι (iota) harfi de seçilebilir), İ yerine I. Böylece eklentisi olmayan oyuncular da senin mesajlarını okuyabilir.

- Oyundaki metin kutularına yazdığın harfler, sen yazarken değişir.
- Sohbet kutusundan gönderdiğin her şeyi eklenti, mesaj gitmeden hemen önce düzeltir; yapıştırdığın metin de buna dahil. Yapıştırdığın metin, sen göndermeden önce kutuda `=` olarak görünür.
- Eklentisi olmayan Türk oyuncuların mesajları ve konuşma balonları senin ekranında `=` yerine harflerle görünür. Bu kısım sadece senin ekranını değiştirir. Bu seçim burada da geçerlidir.

ç, Ç, ö, Ö, ü, Ü ve Ş zaten yazı tipinde var, olduğu gibi kalırlar. Party Finder açıklaması gibi başka kutulara yapıştırdığın metni eklenti düzeltmez.

### Kurulum

1. Oyunda `/xlsettings` yaz, Experimental sekmesini aç.
2. Custom Plugin Repositories altındaki uyarıyı oku, 15 saniyelik bekleme bitince "Ok, I have read and understood this warning" düğmesine bas (yalnızca ilk seferde). Boş satıra `https://raw.githubusercontent.com/ceeprus/ffxiv-turkish-chat/main/repo.json` adresini yapıştır, + düğmesine, ardından sağ alttaki Save düğmesine bas.
3. `/xlplugins` yaz, Turkish Chat Support'u ara ve kur.

Ayarlar için `/xlplugins` içinde Installed Plugins sekmesinde Turkish Chat Support'un yanındaki Settings (dişli) düğmesine bas.
