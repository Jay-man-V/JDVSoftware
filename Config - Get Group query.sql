use [CustomerContact]

-- select * from sec.Application;
-- select * from sec.UserProfile;
-- select * from core.ApplicationConfiguration;
-- select * from core.ConfigurationScope;
-- select * from core.Status;


--declare @key varchar(100) = 'gov.uk.holidays.url'
declare @key varchar(100) = 'email%'
declare @applicationId int = 1
declare @userProfileId int = 2


SELECT
    *
FROM
(
SELECT
    ac.*,
    RANK() OVER (PARTITION BY ac.[Key] ORDER BY ac.ApplicationId DESC, cs.UsageSequence ASC) AS Rnk
FROM
    core.ApplicationConfiguration ac
        INNER JOIN core.ConfigurationScope cs ON
        (
            cs.Id = ac.ConfigurationScopeId
        )
WHERE
    ac.StatusId IN ( 0, 1 ) AND
    GetDate() BETWEEN ac.ValidFrom AND ac.ValidTo AND
    ac.[Key] like @Key AND
    COALESCE ( ac.ApplicationId, 0) IN ( 0, @ApplicationId ) AND
    ac.CreatedByUserProfileId IN ( 1, @userProfileId )
) s
WHERE
    Rnk = 1
ORDER BY
    [Key]
