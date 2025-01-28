public class APIContainer
{
    public static readonly string API_BASE_URL = "http://localhost:8080/"; 

    // Player - container
    public static readonly string API_PLAYER_GET_BY_ID = API_BASE_URL + "api/player/{id}";
    public static readonly string API_PLAYER_SET_BY_ID = API_BASE_URL + "api/player";
    public static readonly string API_GET_ALL_PLAYERS = API_BASE_URL + "api/players";

    // InteractionLog - container
    public static readonly string API_INTERACTION_LOG_GET_BY_ID = API_BASE_URL + "api/log/{id}";
    public static readonly string API_INTERACTION_LOG_SET_BY_ID = API_BASE_URL + "api/log";
    public static readonly string API_GET_ALL_INTERACTION_LOGS = API_BASE_URL + "api/logs";
}
