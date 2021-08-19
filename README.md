# MTG Api project

Use the Magic The Gathering API ( https://magicthegathering.io/ ) to catalog and display the different cards and their attributes.
This API matched the DAE GD Profile as MGT has been ported multiple times as a Video Game. I contains a large collection of cards that has their own attributes, types, category etc. Tools that allow players to search and visuallize cards are really helpful to prepare decks or discover new tactics.

The data available can be displayed through several screens: 
- The cards can be listed and filtered according to the card set selected. (The API limits the get request to 100 records)
- Cards can be filtered by typing their name (or partial name) in the top search bar, pressing ENTER or TAB to submit.
- The filtering by name will be applied to the current Card Set selected.
- Card Sets can be viewed on the left hand side and filtered using the text box above it.
- Selecting the "All" card set allows to perform a name based search on the entirety of MTG collection.

The application can switch between Offline (local json data) and Online (using the MTG API) through the combobox in the bottom left corner. (The Offline data only contains the first 100 cards, all those cards belongs to the "Tenth Edition" set thus the other sets will then be empty.)
