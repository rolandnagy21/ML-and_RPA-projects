Deep Learning projekt összefoglaló (legalul részletesebb bemutatás)
-----------------------------------
<b>Használt library-k: keras, tensorflow, sklearn, seaborn, matplotlib, cv2, pandas, numpy, glob, random </b>

A gyártósor végén kapott elektromos motorokról egy szenzor képeket készít, melyek alapján különböző modellekkel beazonosítjuk a hibás alkatrészeket, illetve klasszifikáljuk őket a hiba típusa szerint. A projektet teljesen üres kóddal kezdjük, így először kialakítjuk a képi adatok megfelelő betöltését, kezelését végző függvényeket. Ezután elemző módszerekkel (pl. ImageGrid) áttekintjük a dataset-t, összehasonlítjuk a class-okat, megvizsgáljuk eloszlásukat.

Ezután felépítjük a különböző modelleket: mindegyik esetében először a dataset adott modellnek megfelelő formára alakítása (array méret, dimenziók, színek), majd train (most 70%) és test (30%) set-re bontás. A neurális hálózatok esetében a labelek-re one-hot encodingot használunk. A betöltések, átalakítások után kapott eredmények helyességét gyakran tesztelő függvényekkel (quality gate) ellenőrízzük. <br>

<b>A következő modellekkel dolgozunk: szupport vektoros gépek klasszifikációra használt verziója (SVC), teljesen összekapcsolt architektúrájú mesterséges neurális hálózatok (ANN, Dense layerek), konvolúciós architektúrájú neurális hálózatok (CNN, konvolúciós layerek és pooling műveletek). </b> <br>
A modellek felépítését nulláról kezdjük (define - rétegek), majd model summary-vel megvizsgáljuk a kialakított régetek architektúráját. Ezután lefordítjuk a felépített modellt (compile - loss, optimizer, metrikák), majd a training set-en tanítjuk (train - epochs, batch méret, validációs set méret). Végül a teszt adatokon (unseen data) végzett teljesítmény kiértékeléséhez classification report-t, és hőtérképes confusion mátrix-t használnuk, írunk. <br>

A magasabb accuracy eléréséhez kísérletezünk a hyperparaméterekkel (activation functoin, batch és epoch méretek, optimezerek és learning rate, rétegek és filterek száma, stb), illetve a rengeteg free paraméter megfelelő tanításához növeljük a dataset-t Data augmentation technikával (ImageDataGenerator).

Pipeline and Regressions projekt összefoglaló 
-----------------------------------
<b>Használt library-k: sklearn, matplotlib, pandas (Series, DataFrame), numpy, seaborn</b>

A use case egy gyártási környezetben használt gép energiafelhasználásnak (kJ) becslése a gép beállított paraméterei alapján, különböző regressziós modellekkel. <br>

Ezt a projektet is üres kóddal kezdjük, így minden függvényt, algoritmust lépésenként haladva, a nulláról építünk fel. Először DataFrame-be betöltjük, és áttekintjük az adatokat, majd az attribútumok szerinti bontásban vizualizáljuk (hisztogram plot) őket. Ezután kezeljük a hiányzó és outlier értékeket, valamint train és test set-re bontjuk a dataset-t.

<b>A következő modellekkel dolgozunk: Linear Regression, Random Forest Regressor, Support Vector Regresssion. </b> Mindegyik esetében a következő lépéseken megyünk végig: 
- importáljuk az adott modellt (sklearn library), és beállítjuk a hyperparamétereit (pl. RF-nél fák száma, SVR-nél kernel típusa)
- tanítjuk a training set-en (fit()) 
- becslünk vele a train és test set-en (azért mindkettőn, hogy a modellek loss mutatóit mindkét esetében össze tudjuk hasonlítani)
- kiszámítjuk a loss mutatókat mindkét set-re (Mean Absolute Error és Mean Squared Error)
- residual plot-on vizualizáljuk a (becsült-valós)/(valós-becsült) reziduális (hiba) értékeket

Ezután egy bar plot-on vizualizáljuk a MAE és MSE mutatókat mindhárom modell esetében a test adatokon, majd ez alapján összehasonlítjuk és kiválasztjuk a legalacsonyabb hibával rendelkezőt. Végül a kiválasztott modell-t "deploy-oljuk", felhasználjuk az eredeti problémára, azaz a gép adott beállítasai alapján megbecsüljük a várható energiafogyasztását. 


Robotic Process Automation projekt összefoglaló
-----------------------------------
Az automatizáló robot megnyitja a megadott weboldalt (most a hasznaltalma.hu), majd az általunk megadott kulcsszóra rákeres a hirdetések között. A találatokat egy saját relációs adatbázisba menti: ID, név, állapot, dátum, ár, illetve Excel-be exportálja ugyanebben a strukturált formában.
A robotot Automation Anywhere-be integráltuk, onnan indítottuk, illetve ott adtuk meg a paramétereket.

ML és RPA projektek teljes mappájának elérése
-----------------------------------
A Notebook-ok nagy méretük miatt lassan töltenek be (pull nélkül) és nehezen olvashatóak (cellák outputjai végig ki vannak írva),
az RPA projekt esetében pedig csak a legrelenvásabb kód fileok találhatóak meg itt, ezért    
a 2 projekt teljes mappáját feltölöttem ide:
https://drive.google.com/drive/folders/11qtNoWj1Z9aUZ6ImwchnyAyrsl4fnAXs?usp=sharing
(a ML projekt esetén tartalmazza a DL dataset-t, egyéb fileokat, saját kiegészítéseket (képek, excelek), 
az RPA projekt esetén pedig az VS Solution minden fileját, mint pl. config fileok)


Kiegészítő információk, saját részek a projektekben
-----------------------------------
A Python megvalósítás közben a mélyebb matematikai háttére is fókuszáltak a tanult anyagok, ezeket az elméleti anyagokat (előadásvideók, slideok) kommentbe (saját megfogalmazással) a kódokhoz igazítottam a kódértelmezés közben, annak érdekében, hogy például a hiperparamétereket értően tudjam változtatni, illetve hogy jobban belelássak a kódok mögötti háttérszámításokba. Kiegészítésképp gyakran utánanéztem a használt library-k dokumentációjában az alkalmazott függvények paramétereinek, működésének, ezeket is kommentben fűztem hozzá a kódhoz.

Időnként kódértelmező kommentjeiben szándékosan túl részletesek, vagy nagyon alapvető Python ismeretekhez utalnak vissza, ezzel az volt a cél, hogy ne csak megértsem a hosszabb kódrészleteket, hanem hogy közben tanuljam magamtól történő megírásukat is, és hogy a kommentek ne csak számomra, hanem külső olvasó számára is minél érthetőbbek legyenek.

A tanulási folyamat közben, mikor egy-egy (számomra) összetettebb Code cellához értem, felette hozzáadtam kiegészítő Code cellákat, melyekben a hosszabb kódsorokat részletekre szedve értelmeztem, próbálgattam ki (printelések, stb), így elősegítve a részletek átlátását, hosszabb kódok értő tanulását. Ezeket a hozzáadott kiegészítő cellákat a transzparencia, olvashatóság érdekében mindig a "saját, értelmezéshez" kommenttel kezdtem.

(A Deep Learning képi adatoknál (Google Drive\Python ML projekt\DL for Image Classification\data\top) a Missing Cover ("_MC_") kategóriájú képek felülnézetben félrevezetőek lehetnek, mert bár innen nézve úgy tűnik, hogy rajtuk van a fekete gumi "takaró", oldalsó nézetből már látszik, hogy nem mindegyik részükön van rajta.)

Deep Learning projekt részletesebb leírása
-----------------------------------
Használt library-k: keras, tensorflow, sklearn, seaborn, matplotlib, cv2, pandas, numpy, glob, random <br> <br>
Elektromos motorokat (autókhoz) gyártását szimuláló környezetben vagyunk, ahol a gyártósor végén kapott motorokról egy szenzor képet készít. Az automatizált gyártás miatt típushibák keletkeznek: missing cover, missing screw, vagy hibamentes, azaz complete - ezek a dataset label-jei, melyek a class-okat is jelentik, hiszen a motorok hibáinak algoritmizált felismerése klasszifikációs problémát jelent.<br><br>
A gépi tanulás modellek felépítését teljesen nulláról kezdjük, így először alaposan átvesszük a képi adatok megfelelő betöltését (feature és label vektorok numpy array-ekkel), kezelését (plottolás imshow metódussal, OpenCV resize, cvtColor, képi adatok array-ekben mentésének sajátosságai, reshape), majd ezután jön az első dataset áttekintés, elemzés, class-ok közötti és class-okon belüli különbségek összehasonlítása (ImageGrid-del), illetve a class eloszlás vizsgálata (Pie chart). <br>
Ezt követi a különböző modellek felépítése: mindegyik esetében először a dataset adott modellnek megfelelő formára alakítása (képeket tároló array-ek mérete, 1 dimenzióra laposítás ha szükséges, színes/szürkeárnyalatos képek, stb), majd train (70%) és test (30%) set-re bontása. A műveletek után kapott eredmények helyességét gyakran tesztelő függvényekkel (quality gate) ellenőrízzük. <br><br>
Az első modell a Szupport Vektoros Gépek, melynél most Radial Basis Function Kernelt használunk, és erős regularizációt választunk (C paraméter alacsony értéke), valamint trainelésnél (fittelés) a classok-nak gyakoriságukkal inverzen arányos súlyokat adunk meg (balanced sample_weight). Az SVC modell, valamint minden további modell teljesítményének kiértékelését a teszt adatokon (unseen data) létrehozott classification report-tal (accuracy), és hőtérképes confusion mátrix-szal végezzük.
<br><br>
A következő modell a neurális hálózatok, azok közül is először a fully connected architektúrájú mesterséges neurális hálózatok (adott réteg minden perceptronja (output) az előző réteg minden perceptronjával (input) kapcsolatban van, van közöttük aktivációs összefüggés). A dataset train és test set-re bontását most a label vektor (class-ok) szerint rétegezve végezzük el, majd a labeleket (classok) one-hot encodingoljuk a Softmax transzformáció (és Cross Entropy) előkészítéseként. Ezután létrehozzuk a szekvenciális modellt, melyhez Dense (sűrű, azaz teljesen összekapcsolt az előző réteggel) layereket adunk hozzá. Az elsőnél megadjuk az input shape-jét, mindegyikhez beállítunk activation function-t (most Rectified Linear Unit, de lehetne pl. Hyperbolic Tangent, vagy Sigmoid), az utolsó layernél megadjuk az output space dimenzionalitását (most a 3 db class miatt 3), activation funcion-jének pedig beállítjuk a Softmax transzformációt, mellyel az egyes classok valószínűségévé alakítjuk a labeleket. A model summary-vel megvizsgáljuk a felépített model architekturáját, azaz az egyes layerek paramétereinek számát (melyeket trainelünk), illetve outputjaik formáját.<br><br>
<b> A részletesebb leírást a magas érdeklődés (kódhoz írt értelmező kommentek is hasonló részletességűek) miatt írom, a projektben a felépített ANN modell lefordítása (loss, optimizer, metrikák), train-elése (epochs, batch méret, validációs set méret), majd a konvolúciós neurális hálózatok (CNN) következnek: szívesen folytatom szóban ilyen, vagy ennél alaposabb módon a projekt bemutatását (például közben a kód mutatásával, értelmezésével) <br>
Nagy Roland Zsolt</b>
