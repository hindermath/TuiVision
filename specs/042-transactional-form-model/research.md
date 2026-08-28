# Research: Transactional Form Model

Magiblot and `tv203s` preserve the classic model: `TDialog` classifies
completion commands and validates before closure; `TInputLine` owns editing,
transfer and validation state. Neither source defines a transactional POCO or
declarative form contract. Therefore the safe design is additive composition:
ordinary controls keep their contracts, while a session and adapters provide
the TuiVision-specific transaction layer.

The exact external checkout was detached at commit
`57b6f56b38e0ee75240a80a10ee0e11470c24693`, tree
`96dd03873955689ff0a79f6c8107a8148fe1ebd6`; COPYRIGHT SHA-256 matched
`66220baeb9761b723fba913b74cf8257621a65c38cadb941fbb5bc181104b548`.
