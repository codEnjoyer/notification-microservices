from typing import Self

from enums import MessageType
from src.schemas.base import BaseSchema, NonEmptyString


class MessageSchema(BaseSchema):
    address: NonEmptyString
    content: NonEmptyString


class BrokerMessage(MessageSchema):
    type: MessageType | None = None

    @classmethod
    def from_message_in(cls, message_in: MessageSchema) -> Self:
        return cls(address=message_in.address, content=message_in.content)
