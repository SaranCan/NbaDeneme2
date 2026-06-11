using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NbaDeneme2.Models;

public partial class Nbadeneme2Context : DbContext
{
    public Nbadeneme2Context()
    {
    }

    public Nbadeneme2Context(DbContextOptions<Nbadeneme2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Arena> Arenas { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Human> Humans { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerGameStat> PlayerGameStats { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamStat> TeamStats { get; set; }

    public virtual DbSet<VwCoachingStaff> VwCoachingStaffs { get; set; }

    public virtual DbSet<VwEastStanding> VwEastStandings { get; set; }

    public virtual DbSet<VwLeagueStanding> VwLeagueStandings { get; set; }

    public virtual DbSet<VwMatchResult> VwMatchResults { get; set; }

    public virtual DbSet<VwPlayerStat> VwPlayerStats { get; set; }

    public virtual DbSet<VwStanding> VwStandings { get; set; }

    public virtual DbSet<VwTeamPayroll> VwTeamPayrolls { get; set; }

    public virtual DbSet<VwTeamRoster> VwTeamRosters { get; set; }

    public virtual DbSet<VwTopAssisterApg> VwTopAssisterApgs { get; set; }

    public virtual DbSet<VwTopBlockerBpg> VwTopBlockerBpgs { get; set; }

    public virtual DbSet<VwTopRebounderRpg> VwTopRebounderRpgs { get; set; }

    public virtual DbSet<VwTopSalary> VwTopSalaries { get; set; }

    public virtual DbSet<VwTopScorer> VwTopScorers { get; set; }

    public virtual DbSet<VwTopScorersPpg> VwTopScorersPpgs { get; set; }

    public virtual DbSet<VwTopStealerSpg> VwTopStealerSpgs { get; set; }

    public virtual DbSet<VwTriggerVerification> VwTriggerVerifications { get; set; }

    public virtual DbSet<VwWestStanding> VwWestStandings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SaranCan;Database=nbadeneme2;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Arena>(entity =>
        {
            entity.HasKey(e => e.ArenaId).HasName("PK__Arenas__F6F7E7879C88163E");

            entity.ToTable(tb => tb.HasTrigger("trg_stamp_arenas"));

            entity.Property(e => e.ArenaId).HasColumnName("ArenaID");
            entity.Property(e => e.ArenaName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Coaches__AA2FFB85C3096A11");

            entity.ToTable(tb => tb.HasTrigger("trg_stamp_coaches"));

            entity.HasIndex(e => e.TeamId, "idx_coaches_team");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("PersonID");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Assistant Coach");
            entity.Property(e => e.Salary).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.Person).WithOne(p => p.Coach)
                .HasForeignKey<Coach>(d => d.PersonId)
                .HasConstraintName("FK__Coaches__PersonI__68487DD7");

            entity.HasOne(d => d.Team).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coaches__TeamID__693CA210");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Contract__C90D3409AD8C1213");

            entity.ToTable(tb => tb.HasTrigger("trg_stamp_contracts"));

            entity.HasIndex(e => e.PersonId, "idx_contracts_person");

            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Salary).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.SeasonId).HasColumnName("SeasonID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.Person).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contracts__Perso__787EE5A0");

            entity.HasOne(d => d.Season).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.SeasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contracts__Seaso__7A672E12");

            entity.HasOne(d => d.Team).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contracts__TeamI__797309D9");
        });

        modelBuilder.Entity<Human>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Human__AA2FFB8577E846D1");

            entity.ToTable("Human", tb => tb.HasTrigger("trg_stamp_human"));

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("PK__Matches__4218C837430D13A6");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_stamp_matches");
                    tb.HasTrigger("trg_update_team_stats");
                });

            entity.HasIndex(e => e.AwayTeamId, "idx_matches_away");

            entity.HasIndex(e => e.HomeTeamId, "idx_matches_home");

            entity.HasIndex(e => e.SeasonId, "idx_matches_season");

            entity.Property(e => e.MatchId).HasColumnName("MatchID");
            entity.Property(e => e.ArenaId).HasColumnName("ArenaID");
            entity.Property(e => e.AwayTeamId).HasColumnName("AwayTeamID");
            entity.Property(e => e.HomeTeamId).HasColumnName("HomeTeamID");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SeasonId).HasColumnName("SeasonID");

            entity.HasOne(d => d.Arena).WithMany(p => p.Matches)
                .HasForeignKey(d => d.ArenaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matches__ArenaID__70DDC3D8");

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.MatchAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matches__AwayTea__6FE99F9F");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.MatchHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matches__HomeTea__6EF57B66");

            entity.HasOne(d => d.Season).WithMany(p => p.Matches)
                .HasForeignKey(d => d.SeasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matches__SeasonI__71D1E811");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Players__AA2FFB8536620716");

            entity.ToTable(tb => tb.HasTrigger("trg_stamp_players"));

            entity.HasIndex(e => new { e.TeamId, e.JerseyNumber }, "UQ__Players__03ACE09C8DFA0ECB").IsUnique();

            entity.HasIndex(e => e.TeamId, "idx_players_team");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("PersonID");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.Person).WithOne(p => p.Player)
                .HasForeignKey<Player>(d => d.PersonId)
                .HasConstraintName("FK__Players__PersonI__5FB337D6");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Players__TeamID__60A75C0F");
        });

        modelBuilder.Entity<PlayerGameStat>(entity =>
        {
            entity.HasKey(e => e.StatsId).HasName("PK__PlayerGa__C23A1F43D5641C45");

            entity.ToTable(tb => tb.HasTrigger("trg_stamp_playergamestats"));

            entity.HasIndex(e => new { e.PersonId, e.MatchId }, "UQ__PlayerGa__CE0E77076B9AD867").IsUnique();

            entity.HasIndex(e => e.MatchId, "idx_pgs_match");

            entity.HasIndex(e => e.PersonId, "idx_pgs_person");

            entity.Property(e => e.StatsId).HasColumnName("StatsID");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MatchId).HasColumnName("MatchID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Match).WithMany(p => p.PlayerGameStats)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerGam__Match__02084FDA");

            entity.HasOne(d => d.Person).WithMany(p => p.PlayerGameStats)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerGam__Perso__01142BA1");
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.SeasonId).HasName("PK__Seasons__C1814E1846E3BA21");

            entity.ToTable(tb => tb.HasTrigger("trg_stamp_seasons"));

            entity.Property(e => e.SeasonId).HasColumnName("SeasonID");
            entity.Property(e => e.ChampionTeamId).HasColumnName("ChampionTeamID");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ChampionTeam).WithMany(p => p.Seasons)
                .HasForeignKey(d => d.ChampionTeamId)
                .HasConstraintName("fk_champion_team");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__123AE7B9DB43CF55");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_no_delete_teams");
                    tb.HasTrigger("trg_stamp_teams");
                });

            entity.HasIndex(e => e.TeamName, "UQ__Teams__4E21CAACFF3CD4F9").IsUnique();

            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.ArenaId).HasColumnName("ArenaID");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Conference)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Division)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Arena).WithMany(p => p.Teams)
                .HasForeignKey(d => d.ArenaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Teams__ArenaID__5535A963");
        });

        modelBuilder.Entity<TeamStat>(entity =>
        {
            entity.HasKey(e => e.TeamStatId).HasName("PK__TeamStat__A0F24FAC31646C76");

            entity.ToTable(tb => tb.HasTrigger("trg_stamp_teamstats"));

            entity.HasIndex(e => new { e.TeamId, e.SeasonId }, "UQ__TeamStat__8E22F359250669F1").IsUnique();

            entity.HasIndex(e => e.SeasonId, "idx_teamstats_season");

            entity.HasIndex(e => e.TeamId, "idx_teamstats_team");

            entity.Property(e => e.TeamStatId).HasColumnName("TeamStatID");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SeasonId).HasColumnName("SeasonID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.Season).WithMany(p => p.TeamStats)
                .HasForeignKey(d => d.SeasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeamStats__Seaso__123EB7A3");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamStats)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeamStats__TeamI__114A936A");
        });

        modelBuilder.Entity<VwCoachingStaff>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_CoachingStaff");

            entity.Property(e => e.CoachName)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.CoachSalary).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.Conference)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Division)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwEastStanding>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_EastStandings");

            entity.Property(e => e.Division)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwLeagueStanding>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_LeagueStandings");

            entity.Property(e => e.Conference)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Division)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwMatchResult>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_MatchResults");

            entity.Property(e => e.AwayTeam)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HomeTeam)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MatchId).HasColumnName("MatchID");
            entity.Property(e => e.Winner)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwPlayerStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_PlayerStats");

            entity.Property(e => e.Apg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("APG");
            entity.Property(e => e.Bpg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("BPG");
            entity.Property(e => e.Mpg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("MPG");
            entity.Property(e => e.PlayerName)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Ppg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("PPG");
            entity.Property(e => e.Rpg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("RPG");
            entity.Property(e => e.Spg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("SPG");
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwStanding>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Standings");

            entity.Property(e => e.Conference)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Division)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwTeamPayroll>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TeamPayroll");

            entity.Property(e => e.Conference)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TotalPayroll).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<VwTeamRoster>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TeamRoster");

            entity.Property(e => e.Conference)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PlayerName)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwTopAssisterApg>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TopAssisterAPG");

            entity.Property(e => e.Apg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("APG");
            entity.Property(e => e.PlayerName)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwTopBlockerBpg>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TopBlockerBPG");

            entity.Property(e => e.Bpg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("BPG");
            entity.Property(e => e.PlayerName)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwTopRebounderRpg>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TopRebounderRPG");

            entity.Property(e => e.PlayerName)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.Rpg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("RPG");
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwTopSalary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TopSalaries");

            entity.Property(e => e.PlayerName)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwTopScorer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TopScorers");

            entity.Property(e => e.PlayerName)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Ppg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("PPG");
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwTopScorersPpg>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TopScorersPPG");

            entity.Property(e => e.PlayerName)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.Ppg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("PPG");
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwTopStealerSpg>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TopStealerSPG");

            entity.Property(e => e.PlayerName)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.Spg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("SPG");
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwTriggerVerification>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TriggerVerification");

            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TriggerStatus)
                .HasMaxLength(8)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwWestStanding>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_WestStandings");

            entity.Property(e => e.Division)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
