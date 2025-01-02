from pydantic import EmailStr

from enums import MessageType
from src.schemas.message import BrokerMessage, MessageSchema


class EmailMessageIn(MessageSchema):
    address: EmailStr


class BrokerEmailMessage(BrokerMessage, EmailMessageIn):
    type: MessageType = MessageType.EMAIL
