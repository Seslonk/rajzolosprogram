#PROJEKTNAPLÓ – Konzolos rajzoló

## Ki mit csinált
- **Csiha Milán Martin:** Én csináltam a képernyő bal oldalán a menüt meg az állapotjelzőt, a kurzor mozgatását, a rajzolást/törlést, a szín- (`Q`) és karakterváltást (`C`), a fájlbetöltést (`r`), a hibakiírásokat és a `Beep` hangokat. Saját funkciónak megcsináltam a `WASD`/nyíl váltást (`y`) és a középre ugrást (`f`).
- **Jászhalmi Szabolcs:** Ő csinálta az `F1–F12` nyomdás részt (a különböző méretű és alakzatú mintákat, meg a kilógás ellenőrzését).
- **Közös munka:** Megbeszéltük, hogy `80×25`-ös konzolban dolgozunk, `56×22`-es rajzlappal, `(0, 0)` a bal felső sarok, összeraktuk a kódot a `main` ágon, és együtt teszteltük.

## Hogyan működik és miket döntöttünk
- Bal oldalon van a menü, alatta a hibaüzenet sora, még lejjebb meg az állapot (hol van a kurzor, mi a karakter, szín, ecset, mód), jobb oldalon meg a `56×22`-es rajzlap a sárga `'O'` kurzorral.
- A nyilakkal lehet léptetni a kurzort (`y`-val átváltható `WASD`-re), az `E` kapcsolja az ecsetet, a `Space` az adott mezőn rajzol vagy töröl, a `Backspace` mindent töröl, az `f` középre (`28, 11`) visz, az `r` fájlból tölt be mintát, az `X` pedig kilép.
- Ha kimegyünk a szélére, rossz/túl nagy fájlt akarunk betölteni, vagy kilóg a nyomda, akkor szól a `Console.Beep()`, kiír egy rövid hibaüzenetet, és nem rontja el a rajzot.

## Tesztelés
- **Szélek:** Kipróbáltuk, hogy a kurzor tényleg megáll-e a rajzlap szélén, és tényleg csippan meg kiírja, hogy `"Határ!"`.
- **Beállítások:** Teszteltük a színváltást (`Q`), a karaktercserét (`C`) úgy, hogy a vezérlőkaraktereket ne fogadja el, meg a `WASD` váltást (`y`) és a középre ugrást (`f`).
- **Fájlbetöltés (`r`):** Kipróbáltuk létező és nem létező `.txt` fájllal, meg túl nagy fájlokkal is – hibánál csippan és megmarad a régi rajz.
- **Sorok pucolása (`C` és `R`):** Korábban ott maradt a szöveg a 24. sorban, ezért átírtuk szóközökre, így már szebb és nem ragad bent.
- **Nyomdák (`F1–F12`):** Teszteltük a különböző mintákat és azt is, hogy ha kilógna a rajzlapról, ne rajzoljon ki félbehagyva.

## AI-használat
- **Csiha Milán Martin:** Kértem AI-segítséget a `Pixel` struct összerakásához, hogy egy helyen legyen a karakter és a szín, meg a `Console.SetWindowSize` és `SetBufferSize` körüli `try-catch`-hez, hogy ne szálljon el más ablakmérettel[cite: 5]. Így már értem, hogyan működik a `Pixel[,]` tömb, és miért kell `try-catch` az ablakméret állításánál.
- **Jászhalmi Szabolcs:**
