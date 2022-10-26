# Bár az open függvény már alapértelmezetten is az aktuális munkakönyvtárban keresi a megnyitandó file-t,
# a teljesség igénye érdekében adjuk meg az importálandó file teljes elérési útját (feltételezve, hogy az is az aktuális munkakönyvtárban található),
# így amennyiben nem sikerül az importálás, a hibaüzenetben látjuk, hogy pontosan milyen elérési úttal kereste a megnyitandó file-t 
from os import getcwd
IPcímek_file = open(f"{getcwd()}\ip.txt", "rt") #rt (read, text) mód már alapértelmezett, de adjuk meg explicit módon is

IPcímek = IPcímek_file.readlines()
IPcímek_file.close()

print(type(IPcímek), type(IPcímek[0]))
print()
print(IPcímek[:5])
print()
# A kapott lista első 5 eleménél már látszik az a probléma, hogy az új sor jelölőt (azaz a "\n"-t, mely a txt fileban rejtett)
# is hozzáírja az IP címek (azaz a sorok) végére. A feladatmegoldás szempontjából ennek nincs jelentősége (mert csak a címek elején keresünk),
# azonban az igényes megoldás érdekében ezt például a következő módon tudjuk kiküszöbölni:
for i in range(len(IPcímek)):
   IPcímek[i] = IPcímek[i].replace("\n", "")
print(IPcímek[:5])
print()

# 1. feladat:
# "Határozza meg, hogy hány adatsor van az állományban."
print(f"{len(IPcímek)} darab adatsor található a txt állományban")
print()

# 2. feladat
# "Határozza meg, hogy az állományban hány darab IP-cím van az egyes fajtákból és az eredményt jelenítse meg a képernyőn."

# 1. módszer: egyezőség keresés a startswith() beepéített metódussal
from collections import defaultdict # a defaultdict alosztályból származtatott szótáraknál,
első_módszer = defaultdict(int)     # ha a szótárban nem létező kulcsnak próbálunk értéket adni, hiba dobása helyett létrehozza a kulcsot a szótárban

for IPcím in IPcímek:
   if IPcím.startswith("2001:0db8"):
      első_módszer["dokumentációs címek"] += 1

   elif IPcím.startswith("2001:0e"):
      első_módszer["globális címek"] += 1

   elif IPcím.startswith("fd") or IPcím.startswith("fc"):
      első_módszer["helyi címek"] += 1

   else:
      első_módszer["egyebek"] += 1

# 2. módszer: egyezőség keresés regurális kifejezésekkel
import re 
második_módszer = defaultdict(int)

for IPcím in IPcímek:
   if re.search("^2001:0db8*", IPcím):  # a '^' metakarakterrel tudunk az 'IPcím' string elején lévő karakterekkel egyezőséget vizsgálni, ne felejtsük az ismeretlen darabszámú karaktereket se ('*')
      második_módszer["dokumentációs címek"] += 1

   elif re.search("^2001:0e*", IPcím):
      második_módszer["globális címek"] += 1

   elif re.search("^fd*", IPcím) or re.search("^fc*", IPcím):
      második_módszer["helyi címek"] += 1

   else:
      második_módszer["egyebek"] += 1

# 3. módszer: Counter használata a startswith() metódus segítségével
from collections import Counter
IPcímek_kategóriáként = []

for IPcím in IPcímek:
   if IPcím.startswith("2001:0db8"):
      IPcímek_kategóriáként.append("dokumentációs címek")
   
   elif IPcím.startswith("2001:0e"):
      IPcímek_kategóriáként.append("globális címek")

   elif IPcím.startswith("fd") or IPcím.startswith("fc"):
      IPcímek_kategóriáként.append("helyi címek")

   else:
       IPcímek_kategóriáként.append("egyebek")

harmadik_módszer = Counter(IPcímek_kategóriáként) # a Counter-rel is gyakorlatilag egy dictionary-t hozunk létre

# A módszerek egyezőségének vizsgálata, és az eredmények kiírása
if len(IPcímek) == sum(első_módszer.values()) == sum(második_módszer.values()) == sum(harmadik_módszer.values()) and \
   0 == első_módszer["egyebek"] == második_módszer["egyebek"] == harmadik_módszer["egyebek"]:

      print("Az összes IP címet összeszámoltuk kategóriánként (szótárral), és nincs olyan cím, melyet ne lehetne besorolni a 3 előre megadott kategóriába")

összeg = 0

for kategória in sorted(harmadik_módszer.keys()): # sorted: ugyanolyan sorrendben írjuk ki a kategóriákat, mint ahogy a feladat szövegében meg voltak adva. Azért a harmadik módszer szótárának kulcsain megyünk végig, mert ennél nem hozta létre az "egyebek" kulcsot, míg az első kettőnél igen, annak ellenére, hogy a hozzá tartozó érték 0 maradt
   if első_módszer[kategória] == második_módszer[kategória] == harmadik_módszer[kategória]:
      print(f"{kategória}: {első_módszer[kategória]} db")
      összeg += első_módszer[kategória]

print("egyebek: {} db".format(első_módszer["egyebek"])) 

if összeg == sum(első_módszer.values()):
   print(f"Összeg: {összeg} db")