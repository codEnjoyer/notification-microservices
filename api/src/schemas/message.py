from src.schemas.base import BaseSchema, NonEmptyString


class MessageSchema(BaseSchema):
    address: NonEmptyString
    content: NonEmptyString
