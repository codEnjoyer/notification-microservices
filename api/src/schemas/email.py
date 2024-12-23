from pydantic import EmailStr

from src.schemas.message import MessageSchema


class EmailMessageSchema(MessageSchema):
    address: EmailStr
