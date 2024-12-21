from pydantic_extra_types.phone_numbers import PhoneNumber

from src.schemas.message import MessageSchema


class TgMessageSchema(MessageSchema):
    address: PhoneNumber
