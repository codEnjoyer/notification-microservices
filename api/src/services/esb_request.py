from uuid import UUID

from enums import ESBRequestState, ESBRequestType
from src.models import ESBRequest
from src.services.base import ModelService
from src.services.repositories.esb_message import ESBRequestRepository
from advanced_alchemy.service.typing import ModelDictT


class ESBRequestService(ModelService[ESBRequest]):
    repository_type = ESBRequestRepository

    async def save_notification_request(
        self,
        notification_data: ModelDictT,
        notification_type: ESBRequestType,
    ) -> ESBRequest:
        request: ESBRequest = await self.create(
            {
                "state": ESBRequestState.SENDING,
                "type": notification_type,
                "body": notification_data.model_dump(),
            },
            auto_commit=True,
        )
        return request

    async def mark_request_failed(self, esb_request_id: UUID) -> ESBRequest:
        request = await self.update(
            {"state": ESBRequestState.SENDING_ERROR},
            esb_request_id,
            auto_commit=True,
        )
        return request

    async def mark_request_sent(self, esb_request_id: UUID) -> ESBRequest:
        request = await self.update(
            {"state": ESBRequestState.SENT},
            esb_request_id,
            auto_commit=True,
        )
        return request
