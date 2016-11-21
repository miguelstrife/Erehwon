# Erehwon
Assesment for CE831

# The Back Story - Erehwon

Until recently the small country of Erehwon (If you are not sure where Erehwon is read it
backwards!) had been ruled by a Tyrant who claimed ownership of all the land in the
country.
Following a bloodless coup, known as “The Fluffy Revolution” the new Republic of Erehwon
has decided to sell off certain blocks of land to the people. Most of the land is priced at the
very reasonable rate of 1 Grouch (the currency of Erehwon) per 10 Hectares. Some more
desirable locations are priced at 5 Grouch per 10 hectares. The most desirable locations are
priced at 1.5 Grumps per 10 hectares (1 Grump = 10 Grouch).
The minimum area an individual can buy is 20 hectares. To avoid all the land being bought by
the one or two individuals who had amassed fortunes under the previousregime, no one
purchaser can buy more than 25% of any given block or more than 500 hectaresin total.
They can purchase this from different locations. The total land area for sale in the first
tranche is 2200 hectares. It consists of 5 blocks of land:
Block One: Is a seaside location with a gently sloping beach on a headland. This is a most
desirable location. Its total size is 400 hectares.
Block Two: Is a location in the foothills of the mountains beside a lake. This is a most
desirable location. Its total size is 250 hectares.
Block Three: Is rolling farmland with good climate and terrain. It is in the middle category.
Its total size is 300 hectares.
Block Four: Is mixed arable land on uneven terrain. It is in the cheapest category. Its size is
1100 hectares.
Block Five: Is industrial land on the outskirts of the one big city in Erehwon. It is in the
cheapest category. Its size is 150 hectares.
You are tasked with building a Website to facilitate the sale of this land. The site should
display pictures of the various Blocks. (I suggest you use Wikimedia Commons
http://commons.wikimedia.org/wiki/Main_Page to source images that you feel are
suitable.)
Individuals must be able to purchase a number of parcels of land blocks in one transaction.
To facilitate this, you will need to implementsome kind of “Shopping Cart” that obeys the
rules.
The site must keep a record of the parcels of land that have been sold and who has bought
them. No area should be oversold.

# The Web Site
The site should be able to do the following:
• Display the blocks that are for sale and the amount of land remaining in the block – how you
do this is up to you
• Enable the user to specify a number of hectares in a given block (subject to the rules already
stated) and put them in a Shopping Cart
• Enable the user to View the Cart
• Enable the user to edit the cart’s contents, e.g. delete purchases or change the number of
hectares against a specific block
• Enable the user to Continue Shopping to add further purchases to the cart.
• The user should be able to proceed to a Checkout
• At some point you will need to gather information about the user, e.g. login name, and
password, Name, address etc. and Credit Card details. These should be stored in the
database, however do not store Credit Card details. (Do NOT attempt to use Secure Sockets,
as digital certificates are required for this with IIS and this technology is not viable in the
labs.)
• At the final stage when the user commits to buying, the items bought should be recorded in
the database and the cart emptied.
• Once the database is updated, the customer should receive a page confirming their
purchases.
• If there is enough time you should also offer the facility for the user to return to the site,
login and then be able to view their order, but NOT the orders of anyone else.
• Note: No maintenance system is required – do NOT offer the facility to edit etc. the
products etc. in the database.

# What You Should Do
Your assignment should complete the tasks specified above.
The application should not crash, nor should you allow foolish data to get into the database,
e.g. users with no name or e-mail address.
Exactly how you design and program the application is up to you.
If you use Web Forms the application should be mainly 3 Tier – although you may directly
access the database from an .aspx page in limited cases, e.g. if you used categories, which were
read from a database and displayed in a drop down list box, you could attach database records
directly to the drop down list box.
Try to ensure that you minimize the repetition of code as much as possible.
This assignment is not an art exam, but:
>> The pages should not look very untidy.
The program should meet basic accessibility standards.
>> You should take usability into consideration in the design of the site.
>> The steps necessary to complete a purchase should be obvious to a novice user.