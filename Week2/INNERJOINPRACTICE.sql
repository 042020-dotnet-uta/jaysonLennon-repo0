SELECT *
FROM Country
CROSS JOIN [State];

SELECT Country.Country
FROM Country
INNER JOIN State as S ON S.CountryID = Country.CountryID;

SELECT *
FROM State
LEFT JOIN Country as C ON C.CountryID = State.CountryID;


SELECT *
FROM State
RIGHT OUTER JOIN Country as C ON C.CountryID = State.CountryID;

SELECT State
FROM State
FULL JOIN Country as C ON C.CountryID = State.CountryID;



