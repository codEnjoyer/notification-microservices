import uuid

from sqlalchemy import Enum, UUID
from sqlalchemy.dialects.postgresql import JSON
from sqlalchemy.orm import Mapped, mapped_column

from enums import ESBRequestState, ESBRequestType
from .base import TimestampMixin


class ESBRequest(TimestampMixin):
    __tablename__ = "esb_requests"

    id: Mapped[uuid.UUID] = mapped_column(
        UUID,
        primary_key=True,
        default=uuid.uuid4,
        comment="ID",
    )
    type: Mapped[ESBRequestType] = mapped_column(
        Enum(ESBRequestType, native_enum=False),
        nullable=False,
        comment="Тип сообщения",
    )
    state: Mapped[ESBRequestState] = mapped_column(
        Enum(ESBRequestState, native_enum=False),
        nullable=False,
        comment="Состояние сообщения",
    )
    body: Mapped[dict] = mapped_column(
        JSON,
        nullable=False,
        comment="Тело сообщения",
    )
