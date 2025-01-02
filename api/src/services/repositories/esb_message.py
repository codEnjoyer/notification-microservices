from src.models import ESBRequest
from src.services.repositories.base import SQLAlchemyRepository


class ESBRequestRepository(SQLAlchemyRepository[ESBRequest]):
    model_type = ESBRequest
